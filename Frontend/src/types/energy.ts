import type { CarbonIntensityIndex, DnoRegion, FuelType, GridRegionType, PlantSource } from './enums';

export interface GenerationMixEntry {
  fuelType: FuelType;
  percentage: number;
}

export interface IntensityData {
  forecast: number;
  actual: number | null;
  index: CarbonIntensityIndex;
  unit: string;
}

export interface RegionIntensity {
  regionType: GridRegionType;
  dnoRegion: DnoRegion;
  intensity: IntensityData;
  generationMix: GenerationMixEntry[];
}

export interface RegionalIntensityResponse {
  retrievedAt: string;
  regions: RegionIntensity[];
}

export interface NationalGenerationMixResponse {
  from: string;
  to: string;
  intensity: IntensityData;
  mix: GenerationMixEntry[];
}

export interface Coordinates {
  latitude: number;
  longitude: number;
}

export interface PowerPlant {
  coordinates: Coordinates;
  name: string;
  operator: string | null;
  plannedEndDate: string | null;
  outputMW: number;
  startDate: string | null;
  source: PlantSource;
}

export interface PowerPlantsResponse {
  plants: PowerPlant[];
}
