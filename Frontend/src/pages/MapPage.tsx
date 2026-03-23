import { useEffect, useMemo, useState } from 'react';
import { ScatterplotLayer } from '@deck.gl/layers';
import MapView from '../components/map/MapView';
import { fetchRegionalIntensity } from '../services/energyService';
import type { RegionIntensity } from '../types/energy';

const INTENSITY_COLOR_MAP: Record<string, [number, number, number, number]> = {
  'very low': [0, 200, 0, 200],
  'low': [100, 220, 50, 200],
  'moderate': [255, 200, 0, 200],
  'high': [255, 100, 0, 200],
  'very high': [255, 30, 30, 200],
};

export default function MapPage() {
  const [regions, setRegions] = useState<RegionIntensity[]>([]);

  useEffect(() => {
    fetchRegionalIntensity()
      .then((data) => setRegions(data.regions))
      .catch(console.error);
  }, []);

  const layers = useMemo(
    () => [
      new ScatterplotLayer<RegionIntensity>({
        id: 'regional-intensity',
        data: regions,
        getPosition: (d) => [d.longitude, d.latitude],
        getRadius: (d) => d.intensity.forecast * 100,
        getFillColor: (d) =>
          INTENSITY_COLOR_MAP[d.intensity.index] ?? [128, 128, 128, 180],
        radiusMinPixels: 8,
        radiusMaxPixels: 40,
        pickable: true,
      }),
    ],
    [regions],
  );

  return (
    <div style={{ width: '100vw', height: '100vh' }}>
      <MapView layers={layers} />
    </div>
  );
}
