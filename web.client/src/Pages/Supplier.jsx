import { useEffect, useState } from "react";
import { useParams } from 'react-router-dom';

import Spinner from 'react-bootstrap/Spinner';
import Ratio from 'react-bootstrap/Ratio';

import ProductCard from "../Components/ProductCard";
import ProductModal from "../Components/ProductModal";
import CartPreview from "../Components/CartPreview";
import SearchBar from "../Components/SearchBar";


export default function Supplier() {

    const [unfilteredProducts, setUnfilteredProducts] = useState([])
    const [products, setProducts] = useState([]);

    const [supplier, setSupplier] = useState(null);
    const [description, setDescription] = useState(null);
    const [error, setError] = useState(null);

    const [isLoading, setIsLoading] = useState(true);

    const [clickedProductIndex, setClickedProductIndex] = useState(-1);
    const [selectedCategoryIndex, setSelectedCategoryIndex] = useState(0);

    const { slug, id } = useParams();

    const [categories, setCategories] = useState([]);

    const handleSearch = (query) => {

        if (query === "") {
            if (products.length === unfilteredProducts.length)
                return;
            setProducts(unfilteredProducts);
        }
        else
            setProducts(unfilteredProducts.filter(x => x.name.toLowerCase().includes(query.toLowerCase())));
    };

    const handleCategoryChanged = (categoryIndex) => {
        setSelectedCategoryIndex(categoryIndex);
    }

    const fetchSupplier = async () => {
        try {
            const response = await fetch(`https://localhost:7184/Supplier/GetSupplier?supplierId=` + id);
            if (!response.ok)
                throw new Error(`Error: ${response.status}`);
            const data = await response.json();
            setSupplier(data);
            fetchProducts();
            setError(null);
        }
        catch (error) {
            /*  console.log(error.message); */
            setError(error.message);
        }
        finally {
            setIsLoading(false);
        }
    };

    const fetchProducts = async () => {
        try {
            const response = await fetch('https://localhost:7184/Product?supplierId=' + id);
            if (!response.ok)
                throw new Error(`Error: ${response.status}`);
            const data = await response.json();
            setUnfilteredProducts(data);
            setProducts(data);
            setError(null);
        }
        catch (error) {
            console.log(error.message);
        }
        finally {
            setIsLoading(false);
        }
    };

    //get supplier from redux store
    useEffect(() => {
        fetchSupplier();
    }, []);

    useEffect(() => {
        const categorySet = new Set();
        products.forEach(product => categorySet.add(product.category));
        setCategories(Array.from(categorySet));
    }, [products])

    return (
        <>
            {isLoading ? <Spinner className="spinner" animation="border" /> :
                <div className="supplier-page-container">
                    { // Top Part
                        supplier &&
                        <div className="supplier-top-container">
                            <Ratio aspectRatio="21x9">
                                <img src={"data:image/jpeg;base64," + supplier.image} />
                            </Ratio>
                            <div className="supplier-white-board">
                                <h2>{supplier.name}</h2>
                                <p>asdasdasdasdasd ads ASd A dasd AS dASd as DASD a dasd ASD ASD sd dasd asd </p>
                            </div>
                        </div>
                    }

                    <div className="category-displayer">
                        <div className="category-displayer-title">
                            <i className="bi bi-card-list" />
                            <h4>Categorii</h4>
                        </div>
                        {categories.map((category, index) => ( // method to navigate to this (<LINK to=/#category-name/>)
                            <div key={index} onClick={() => handleCategoryChanged(index)}>
                                <p className={selectedCategoryIndex === index ? "selected-category" : ""}>{category}</p>
                            </div>
                        ))}
                    </div>

                    <div className="category-parent">
                        <SearchBar onSearch={handleSearch} />
                        {categories?.map((category, index) => (
                            <div key={index} className="category-container">
                                <h4>{category}</h4>
                                <div className="products-container">
                                    {products.filter(product => product.category === category).map((product, index) => (
                                        <ProductCard key={index}
                                            product={product}
                                            onClick={() => setClickedProductIndex(index)} />
                                    ))}
                                </div>
                            </div>
                        ))}
                    </div>
                    <CartPreview supplierId={supplier?.id} className='supplier-cart-preview' />
                </div>
            }

            {clickedProductIndex > -1 && <ProductModal
                show={clickedProductIndex > -1}
                onHide={() => setClickedProductIndex(-1)}
                product={products[clickedProductIndex]} />}
        </>
    )
};
