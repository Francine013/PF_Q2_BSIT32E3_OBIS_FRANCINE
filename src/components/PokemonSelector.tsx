interface PokemonSelectorProps {
    onSelect: (name: string) => void;
}

const options = [
    'pikachu', 'bulbasaur', 'charmander', 'squirtle', 'butterfree', 
    'pidgeot', 'arcanine', 'gengar', 'gyarados', 'snorlax', 
    'dragonite', 'mewtwo', 'lucario', 'greninja'
];

export default function PokemonSelector({ onSelect }: PokemonSelectorProps) {
    return (
        <div style={{
            margin: '30px 0',
            fontFamily: 'Georgia, serif',
            textAlign: 'center'
        }}>
            <label style={{
                marginRight: '15px',
                color: '#8E4453', // Deep Rose
                fontSize: '1.1rem',
                fontStyle: 'italic',
                fontWeight: '600'
            }}>
                Select Your Pokémon:
            </label>

            <select
                onChange={(e) => onSelect(e.target.value)}
                defaultValue=""
                style={{
                    padding: '10px 20px',
                    borderRadius: '25px', // Rounded pill shape para mas modern/formal
                    border: '2px solid #F8BBD0',
                    backgroundColor: '#FFFFFF',
                    color: '#4A4A4A',
                    fontSize: '1rem',
                    cursor: 'pointer',
                    outline: 'none',
                    boxShadow: '0 2px 5px rgba(212, 119, 139, 0.1)',
                    transition: 'border-color 0.3s ease, box-shadow 0.3s ease',
                    appearance: 'none', // Tinatanggal ang default arrow sa ibang browsers
                    WebkitAppearance: 'none',
                    textAlign: 'center'
                }}
                onFocus={(e) => {
                    e.target.style.borderColor = '#D4778B';
                    e.target.style.boxShadow = '0 0 8px rgba(212, 119, 139, 0.3)';
                }}
                onBlur={(e) => {
                    e.target.style.borderColor = '#F8BBD0';
                    e.target.style.boxShadow = '0 2px 5px rgba(212, 119, 139, 0.1)';
                }}
            >
                <option value="" disabled>— Choose an entry —</option>
                {options.map(name => (
                    <option
                        key={name}
                        value={name}
                        style={{ textTransform: 'capitalize' }}
                    >
                        {name.charAt(0).toUpperCase() + name.slice(1)}
                    </option>
                ))}
            </select>
        </div>
    );
}