import { useCallback, useEffect, useMemo, useState } from 'react';
import { GeoJsonLayer } from '@deck.gl/layers';
import MapView from '../components/map/MapView';
import RegionDetailPanel from '../components/map/RegionDetailPanel';
import { fetchRegionalIntensity } from '../services/energyService';
import type { RegionIntensity } from '../types/energy';
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

export default function MapPage() {
  const [regions, setRegions] = useState<RegionIntensity[]>([]);
  const [hoveredRegion, setHoveredRegion] = useState<RegionIntensity | null>(null);
  const [hoveredRegionName, setHoveredRegionName] = useState<string | null>(null);

  useEffect(() => {
    fetchRegionalIntensity()
      .then((data) => setRegions(data.regions))
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

  const layers = useMemo(() => {
    return [
      new GeoJsonLayer({
        id: 'regional-intensity',
        data: regionsGeoJson,
        filled: true,
        stroked: true,
        getFillColor: (f) => {
          const region = lookup.get(f.properties.regionType);
          if (!region) return [128, 128, 128, 100];
          return INTENSITY_COLOR_MAP[region.intensity.index] ?? [128, 128, 128, 180];
        },
        getLineColor: [40, 40, 40, 200],
        getLineWidth: 1,
        lineWidthMinPixels: 1,
        pickable: true,
        autoHighlight: true,
        highlightColor: [255, 255, 255, 10],
        onHover,
        updateTriggers: {
          getFillColor: [regions],
        },
      }),
    ];
  }, [regions, lookup, onHover]);

  return (
    <div style={{ display: 'flex', width: '100%', height: '600px', borderRadius: '8px', overflow: 'hidden' }}>
      <div style={{ flex: 1, position: 'relative' }}>
        <MapView layers={layers} />
      </div>
      <RegionDetailPanel region={hoveredRegion} regionName={hoveredRegionName} />
    </div>
  );
}
