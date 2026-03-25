import { useControl } from 'react-map-gl/maplibre';
import { MapboxOverlay } from '@deck.gl/mapbox';
import type { Layer } from '@deck.gl/core';

type TooltipContent = null | string | { text?: string; html?: string; className?: string; style?: Partial<CSSStyleDeclaration> };

interface DeckOverlayProps {
  layers: Layer[];
  getTooltip?: (info: any) => TooltipContent;
}

export default function DeckOverlay({ layers, getTooltip }: DeckOverlayProps) {
  const overlay = useControl(() => new MapboxOverlay({ interleaved: false }));
  overlay.setProps({ layers, getTooltip });
  return null;
}
