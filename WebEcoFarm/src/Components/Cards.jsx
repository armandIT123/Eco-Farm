import { useState, useEffect } from 'react';
/*import { useSlug } from '../Utils';*/

import CardItem from './CardItem';
import Spinner from 'react-bootstrap/Spinner';
import { useGetAllSuppliersQuery } from '../Features/suppliersApi';

function useSlug(name) {
    return name
        .toLowerCase()
        .trim()
        .replace(/[^a-z0-9]+/g, '-')
        .replace(/^-+|-+$/g, '');
}

function Cards() {

    const { data, error, isLoading } = useGetAllSuppliersQuery();

    return (
        <>
            <div className='cards'>
                {isLoading && <Spinner animation="border" />}
                {error && <p>{"A aparut o eroare: " + error.error}</p>}

                <div className='cards__container'>
                    <div className='cards__wrapper'>
                        <ul className='cards__items'>
                            {data?.map((supplier, index) => (
                                <CardItem key={index}
                                    src={"data:image/jpeg;base64," + supplier.image}
                                    text={supplier.name}
                                    label={supplier.id}
                                    path={"/descopera/" + useSlug(supplier.name) + "/" + supplier.id}
                                />
                            ))}
                        </ul>
                    </div>
                </div>
            </div>
        </>
    );
}

export default Cards;