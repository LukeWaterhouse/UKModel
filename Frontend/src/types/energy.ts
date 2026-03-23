export interface GenerationMixEntry {
  fuel: string;
  percentage: number;
}

export interface IntensityData {
  forecast: number;
  actual: number | null;
  index: string;
}

export interface RegionIntensity {
  regionId: number;
  name: string;
  dnoRegion: string;
  longitude: number;
  latitude: number;
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
  mix: GenerationMixEntry[];
}
