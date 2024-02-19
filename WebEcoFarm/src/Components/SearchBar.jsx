import React, { useEffect, useState } from 'react'

export default function SearchBar({ onSearch }) {

    const [searchTerm, setSearchTerm] = useState('');

    useEffect(() => {
        const delayDebounceFn = setTimeout(() => {
            onSearch(searchTerm.trim());
        }, 500)

        return () => clearTimeout(delayDebounceFn)
    }, [searchTerm])

    return (
        <div className='searchbar-container'>
            <i className="bi bi-search"></i>
            <input
                placeholder='Caut/a produse'
                value={searchTerm}
                onChange={(e) => setSearchTerm(e.target.value)} />
            <i className={"bi bi-x-circle delete-search-input"} onClick={() => setSearchTerm("")}></i>
        </div>
    )
}
