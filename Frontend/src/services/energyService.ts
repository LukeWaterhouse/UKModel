import type { RegionalIntensityResponse } from '../types/energy';

const API_BASE = 'http://localhost:5003';

export async function fetchRegionalIntensity(): Promise<RegionalIntensityResponse> {
  const response = await fetch(`${API_BASE}/api/v1/energy/regions`);
  if (!response.ok) {
    throw new Error(`Failed to fetch regional intensity: ${response.status}`);
  }
  return response.json();
}
