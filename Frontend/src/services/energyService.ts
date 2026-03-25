import type { NationalGenerationMixResponse, PowerPlantsResponse, RegionalIntensityResponse } from '../types/energy';

const API_BASE = 'http://localhost:5003';

export async function fetchRegionalIntensity(): Promise<RegionalIntensityResponse> {
  const response = await fetch(`${API_BASE}/api/v1/energy/regions`);
  if (!response.ok) {
    throw new Error(`Failed to fetch regional intensity: ${response.status}`);
  }
  return response.json();
}

export async function fetchNationalGenerationMix(): Promise<NationalGenerationMixResponse> {
  const response = await fetch(`${API_BASE}/api/v1/energy/national`);
  if (!response.ok) {
    throw new Error(`Failed to fetch national generation mix: ${response.status}`);
  }
  return response.json();
}

export async function fetchPowerPlants(): Promise<PowerPlantsResponse> {
  const response = await fetch(`${API_BASE}/api/v1/energy/power-plants`);
  if (!response.ok) {
    throw new Error(`Failed to fetch power plants: ${response.status}`);
  }
  return response.json();
}
