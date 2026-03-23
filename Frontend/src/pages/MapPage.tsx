import { useCallback, useEffect, useMemo, useState } from 'react';
import { GeoJsonLayer } from '@deck.gl/layers';
import MapView from '../components/map/MapView';
import RegionDetailPanel from '../components/map/RegionDetailPanel';
import NationalSummary from '../components/NationalSummary';
import { fetchRegionalIntensity, fetchNationalGenerationMix } from '../services/energyService';
import type { RegionIntensity, NationalGenerationMixResponse } from '../types/energy';
import { CarbonIntensityIndex, GridRegionType } from '../types/enums';
import regionsGeoJson from '../assets/uk-grid-regions.geojson';

const REGION_ID_TO_NAME = Object.fromEntries(
  Object.entries(GridRegionType).map(([name, id]) => [id, name]),
) as Record<number, string>;

const REGION_DISPLAY_NAME: Record<string, string> = {
  NorthScotland: 'North Scotland',
  SouthScotland: 'South Scotland',
  NorthWestEngland: 'North West England',
  NorthEastEngland: 'North East England',
  Yorkshire: 'Yorkshire',
  NorthWales: 'North Wales',
  SouthWales: 'South Wales',
  WestMidlands: 'West Midlands',
  EastMidlands: 'East Midlands',
  EastEngland: 'East England',
  SouthWestEngland: 'South West England',
  SouthEngland: 'South England',
  London: 'London',
  SouthEastEngland: 'South East England',
  England: 'England',
  Scotland: 'Scotland',
  Wales: 'Wales',
};

const INTENSITY_COLOR_MAP: Record<number, [number, number, number, number]> = {
  [CarbonIntensityIndex.VeryLow]: [0, 200, 0, 200],
  [CarbonIntensityIndex.Low]: [100, 220, 50, 200],
  [CarbonIntensityIndex.Moderate]: [255, 200, 0, 200],
  [CarbonIntensityIndex.High]: [255, 100, 0, 200],
  [CarbonIntensityIndex.VeryHigh]: [255, 30, 30, 200],
};

// Convert HSL (hue 0-360, saturation 0-100, lightness 0-100) to RGB [0-255, 0-255, 0-255].
// Uses the analytical HSL→RGB formula without branching on hue sextants.
function hslToRgb(h: number, s: number, l: number): [number, number, number] {
  s /= 100;
  l /= 100;
  const k = (n: number) => (n + h / 30) % 12;
  const a = s * Math.min(l, 1 - l);
  const f = (n: number) => l - a * Math.max(-1, Math.min(k(n) - 3, 9 - k(n), 1));
  return [Math.round(f(0) * 255), Math.round(f(8) * 255), Math.round(f(4) * 255)];
}

// Map a backend intensity index (0–4) to an RGBA color using an HSL gradient.
// The index is the authority — it picks the color band (green/lime/yellow/orange/red).
// The continuous value (actual if available, otherwise forecast) shades within that band
// for visual nuance — regions with the same index but different values look subtly different.
//
// Band thresholds (from Carbon Intensity API):
//   VeryLow: 0–50, Low: 51–100, Moderate: 101–200, High: 201–300, VeryHigh: 301+
//
// Hue:        120° (green) → 0° (red), spread across and within index bands.
// Saturation: 80% → 90%, slightly more vivid at the red/dangerous end.
// Lightness:  48% → 38%, darker towards red so high-carbon regions feel more ominous.
const BAND_RANGES: Record<number, [number, number]> = {
  [CarbonIntensityIndex.VeryLow]:  [0, 50],
  [CarbonIntensityIndex.Low]:      [51, 100],
  [CarbonIntensityIndex.Moderate]: [101, 200],
  [CarbonIntensityIndex.High]:     [201, 300],
  [CarbonIntensityIndex.VeryHigh]: [301, 400],
};

function indexToColor(index: number, actual: number | null, forecast: number): [number, number, number, number] {
  const band = BAND_RANGES[index];
  const value = actual ?? forecast;
  // Position within the band (0–1)
  const bandT = band
    ? Math.min(1, Math.max(0, (value - band[0]) / (band[1] - band[0])))
    : 0.5;
  // Combine index step + sub-band position for a continuous 0–1 value across all 5 bands
  const t = (Math.min(index, 4) + bandT) / 5;
  const hue = 120 * (1 - t);
  const sat = 80 + t * 10;
  const lit = 48 - t * 10;
  const [r, g, b] = hslToRgb(hue, sat, lit);
  return [r, g, b, 200];
}

export default function MapPage() {
  const [regions, setRegions] = useState<RegionIntensity[]>([]);
  const [hoveredRegion, setHoveredRegion] = useState<RegionIntensity | null>(null);
  const [hoveredRegionName, setHoveredRegionName] = useState<string | null>(null);
  const [pinnedRegion, setPinnedRegion] = useState<RegionIntensity | null>(null);
  const [pinnedRegionName, setPinnedRegionName] = useState<string | null>(null);
  const [pinnedRegionKey, setPinnedRegionKey] = useState<string | null>(null);
  const [nationalMix, setNationalMix] = useState<NationalGenerationMixResponse | null>(null);

  useEffect(() => {
    fetchRegionalIntensity()
      .then((data) => setRegions(data.regions))
      .catch(console.error);
    fetchNationalGenerationMix()
      .then(setNationalMix)
      .catch(console.error);
  }, []);

  const lookup = useMemo(() => {
    const map = new Map<string, RegionIntensity>();
    for (const r of regions) {
      const name = REGION_ID_TO_NAME[r.regionType];
      if (name) map.set(name, r);
    }
    return map;
  }, [regions]);

  const onHover = useCallback(
    (info: { object?: { properties?: { regionType?: string } } }) => {
      const regionKey = info.object?.properties?.regionType ?? null;
      if (regionKey) {
        const region = lookup.get(regionKey) ?? null;
        setHoveredRegion(region);
        setHoveredRegionName(REGION_DISPLAY_NAME[regionKey] ?? regionKey);
      } else {
        setHoveredRegion(null);
        setHoveredRegionName(null);
      }
    },
    [lookup],
  );

  const onClick = useCallback(
    (info: { object?: { properties?: { regionType?: string } } }) => {
      const regionKey = info.object?.properties?.regionType ?? null;
      if (!regionKey) {
        setPinnedRegion(null);
        setPinnedRegionName(null);
        setPinnedRegionKey(null);
        return;
      }
      const region = lookup.get(regionKey) ?? null;
      if (pinnedRegion && pinnedRegionName === (REGION_DISPLAY_NAME[regionKey] ?? regionKey)) {
        setPinnedRegion(null);
        setPinnedRegionName(null);
        setPinnedRegionKey(null);
      } else {
        setPinnedRegion(region);
        setPinnedRegionName(REGION_DISPLAY_NAME[regionKey] ?? regionKey);
        setPinnedRegionKey(regionKey);
      }
    },
    [lookup, pinnedRegion, pinnedRegionName],
  );

  const layers = useMemo(() => {
    return [
      new GeoJsonLayer({
        id: 'regional-intensity',
        data: regionsGeoJson,
        filled: true,
        stroked: true,
        getFillColor: (f) => {
          const key = f.properties.regionType;
          const region = lookup.get(key);
          if (!region) return [128, 128, 128, 100];
          const base = indexToColor(region.intensity.index, region.intensity.actual, region.intensity.forecast);
          if (pinnedRegionKey === key) return [base[0], base[1], base[2], 255] as [number, number, number, number];
          return base;
        },
        getLineColor: (f) => {
          if (pinnedRegionKey === f.properties.regionType) return [255, 255, 255, 220];
          return [40, 40, 40, 200];
        },
        getLineWidth: (f) => {
          if (pinnedRegionKey === f.properties.regionType) return 3;
          return 1;
        },
        lineWidthMinPixels: 1,
        pickable: true,
        autoHighlight: true,
        highlightColor: [255, 255, 255, 10],
        onHover,
        onClick,
        updateTriggers: {
          getFillColor: [regions, pinnedRegionKey],
          getLineColor: [pinnedRegionKey],
          getLineWidth: [pinnedRegionKey],
        },
        transitions: {
          getFillColor: { duration: 800 },
          getLineColor: { duration: 300 },
          getLineWidth: { duration: 300 },
        },
      }),
    ];
  }, [regions, lookup, onHover, onClick, pinnedRegionKey]);

  return (
    <div style={{ width: '100%', overflow: 'hidden', borderRadius: '8px' }}>
      <NationalSummary data={nationalMix} />
      <div style={{ display: 'flex', height: '600px' }}>
        <div style={{ flex: 1, position: 'relative' }}>
          <MapView layers={layers} />
        </div>
        <RegionDetailPanel
          region={pinnedRegion ?? hoveredRegion}
          regionName={pinnedRegionName ?? hoveredRegionName}
          pinned={pinnedRegion !== null}
          onUnpin={() => { setPinnedRegion(null); setPinnedRegionName(null); setPinnedRegionKey(null); }}
        />
      </div>
    </div>
  );
}
