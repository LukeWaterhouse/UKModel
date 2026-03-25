import Map from 'react-map-gl/maplibre';
import 'maplibre-gl/dist/maplibre-gl.css';
import type { Layer } from '@deck.gl/core';

type TooltipContent = null | string | { text?: string; html?: string; className?: string; style?: Partial<CSSStyleDeclaration> };
import DeckOverlay from './DeckOverlay';

const MAP_STYLE = 'https://basemaps.cartocdn.com/gl/dark-matter-gl-style/style.json';

const INITIAL_VIEW_STATE = {
  longitude: -2.5,
  latitude: 54,
  zoom: 5,
};

interface MapViewProps {
  layers: Layer[];
  getTooltip?: (info: any) => TooltipContent;
  onZoomChange?: (zoom: number) => void;
}

export default function MapView({ layers, getTooltip, onZoomChange }: MapViewProps) {
  return (
    <Map
      initialViewState={INITIAL_VIEW_STATE}
      mapStyle={MAP_STYLE}
      style={{ width: '100%', height: '100%' }}
      cursor="auto"
      onMove={(e) => onZoomChange?.(e.viewState.zoom)}
    >
      <DeckOverlay layers={layers} getTooltip={getTooltip} />
    </Map>
  );
}
