import { useState } from 'react';
import PokemonCard from './components/PokemonCard';
import PokemonSelector from './components/PokemonSelector';
import type { Pokemon } from './types';

export default function App() {
  const [pokemon, setPokemon] = useState<Pokemon | null>(null);

  // This function is passed DOWN to PokemonSelector
  // The child calls it with the selected name
  function handleSelect(name: string) {
    if (!name) return; // Prevent fetching list when default option is selected

    fetch(`http://localhost:5198/api/pokemon/${name}`)
      .then(res => res.json())
      .then(data => {
        const clean: Pokemon = {
          id: data.id,
          name: data.name,
          height: data.height,
          weight: data.weight,
          imageUrl: data.imageUrl
        };
        setPokemon(clean);
      });
  }

  return (
    <div style={{ display: 'flex', flexDirection: 'column', alignItems: 'center', marginTop: '50px', fontFamily: 'sans-serif' }}>
      <h1>Pokémon Explorer</h1>
      {/* Pass the function DOWN as a prop */}
      <PokemonSelector onSelect={handleSelect} />
      {/* Conditionally render the card once we have data */}
      {pokemon && <PokemonCard pokemon={pokemon} />}
    </div>
  );
}
