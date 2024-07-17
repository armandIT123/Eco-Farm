import React, { useState } from 'react'
import FloatingInput from '../Components/FloatingInput';

function NameInput({ onNext }) {

    const [name, setName] = useState('');
    const [invalidName, setInvalidName] = useState(true);

    const handleNameChande = (value) => {
        setName(value);
        setInvalidName(value.length === 0);
    }

    return (
        <div className='name-input-container'
            onKeyDown={(e) => !invalidName && e.key == 'Enter' ? onNext('next', name) : null}>

            <i onClick={() => onNext('back', '')} className="bi bi-arrow-left-circle"></i>
            <h2>Spune-ne numele tău</h2>
            <p>Vrem sa știm cum ne adresăm</p>
            <FloatingInput label='Nume' type='text' icon='bi bi-envelope' onValueChange={handleNameChande} />
            <button className={invalidName ? 'invalid-name' : ''} onClick={() => onNext('next', name)}>
                Continuă
            </button>
        </div>
    )
}

export default NameInput