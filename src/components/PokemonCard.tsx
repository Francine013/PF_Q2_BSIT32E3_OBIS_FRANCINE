import type { Pokemon } from '../types';

interface PokemonCardProps {
    pokemon: Pokemon;
}

export default function PokemonCard({ pokemon }: PokemonCardProps) {
    return (
        <div style={{
            /* Glassmorphism & Gradient Effect */
            background: 'linear-gradient(135deg, #FFF5F7 0%, #FCE4EC 100%)',
            border: '1px solid #F8BBD0',
            borderRadius: '16px',
            padding: '24px',
            marginTop: '20px',
            textAlign: 'center',
            boxShadow: '0 10px 25px rgba(212, 119, 139, 0.15)',
            transition: 'transform 0.3s ease, box-shadow 0.3s ease',
            maxWidth: '300px',
            margin: '20px auto',
            color: '#4A4A4A', // Soft Charcoal para sa text
            fontFamily: 'Georgia, serif'
        }}>
            {/* Pokemon Name with accent color */}
            <h2 style={{
                textTransform: 'capitalize',
                margin: '0 0 15px 0',
                color: '#8E4453', // Deep Wine
                fontSize: '1.8rem',
                borderBottom: '2px solid #F8BBD0',
                paddingBottom: '10px'
            }}>
                {pokemon.name}
            </h2>

            {/* Image Container with subtle glow */}
            <div style={{
                background: 'rgba(255, 255, 255, 0.5)',
                borderRadius: '50%',
                padding: '10px',
                display: 'inline-block',
                marginBottom: '15px',
                boxShadow: 'inset 0 0 10px rgba(0,0,0,0.05)'
            }}>
                <img
                    src={pokemon.imageUrl}
                    alt={pokemon.name}
                    style={{ width: '250px', height: '255px', objectFit: 'contain' }}
                />
            </div>

            {/* Stats Section with better spacing */}
            <div style={{ textAlign: 'left', padding: '0 10px' }}>
                <p style={{ margin: '8px 0', fontSize: '0.95rem' }}>
                    <strong style={{ color: '#D4778B' }}>ID:</strong> #{pokemon.id.toString().padStart(3, '0')}
                </p>
                <p style={{ margin: '8px 0', fontSize: '0.95rem' }}>
                    <strong style={{ color: '#D4778B' }}>Height:</strong> {pokemon.height / 10} m
                </p>
                <p style={{ margin: '8px 0', fontSize: '0.95rem' }}>
                    <strong style={{ color: '#D4778B' }}>Weight:</strong> {pokemon.weight / 10} kg
                </p>
            </div>
        </div>
    );
}