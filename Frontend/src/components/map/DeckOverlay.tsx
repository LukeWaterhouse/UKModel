import { useControl } from 'react-map-gl/maplibre';
import { MapboxOverlay } from '@deck.gl/mapbox';
import type { Layer } from '@deck.gl/core';
import type { TooltipContent } from '@deck.gl/core/typed';

interface DeckOverlayProps {
  layers: Layer[];
  getTooltip?: (info: any) => TooltipContent;
}

export default function DeckOverlay({ layers, getTooltip }: DeckOverlayProps) {
  const overlay = useControl(() => new MapboxOverlay({ interleaved: false }));
  overlay.setProps({ layers, getTooltip });
  return null;
}
