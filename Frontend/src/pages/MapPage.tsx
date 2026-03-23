import { useEffect, useMemo, useState } from 'react';
import { GeoJsonLayer } from '@deck.gl/layers';
import MapView from '../components/map/MapView';
import { fetchRegionalIntensity } from '../services/energyService';
import type { RegionIntensity } from '../types/energy';
import { CarbonIntensityIndex, GridRegionType } from '../types/enums';
import regionsGeoJson from '../assets/uk-grid-regions.geojson';

const REGION_ID_TO_NAME = Object.fromEntries(
  Object.entries(GridRegionType).map(([name, id]) => [id, name]),
) as Record<number, string>;

const INTENSITY_COLOR_MAP: Record<number, [number, number, number, number]> = {
  [CarbonIntensityIndex.VeryLow]: [0, 200, 0, 200],
  [CarbonIntensityIndex.Low]: [100, 220, 50, 200],
  [CarbonIntensityIndex.Moderate]: [255, 200, 0, 200],
  [CarbonIntensityIndex.High]: [255, 100, 0, 200],
  [CarbonIntensityIndex.VeryHigh]: [255, 30, 30, 200],
};

export default function MapPage() {
  const [regions, setRegions] = useState<RegionIntensity[]>([]);

  useEffect(() => {
    fetchRegionalIntensity()
      .then((data) => setRegions(data.regions))
      .catch(console.error);
  }, []);

  const layers = useMemo(() => {
    const lookup = new Map<string, RegionIntensity>();
    for (const r of regions) {
      const name = REGION_ID_TO_NAME[r.regionType];
      if (name) lookup.set(name, r);
    }

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
        updateTriggers: {
          getFillColor: [regions],
        },
      }),
    ];
  }, [regions]);

  return (
    <div style={{ width: '100%', height: '600px', borderRadius: '8px', overflow: 'hidden' }}>
      <MapView layers={layers} />
    </div>
  );
}
