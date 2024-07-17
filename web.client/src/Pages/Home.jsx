import Button from "../Components/Button"
import FloatingInput from "../Components/FloatingInput";
import Slider from "../Components/Slider"

const Home = () => {
    return (
        <>
            <div className='hero-container'>
                <div className="hero-text">
                    <h2>Produse naturale direct la ușa ta !</h2>
                    <Button buttonStyle="btn--primary">Descoperă produsele</Button>
                </div>
                <Slider images={["homeImage.jpeg", "testImage.jpg"]} />
            </div>
            <div className="sd-container">
                <div className="sd">
                    <i className="bi bi-bag sd-icon"></i>
                    <h4>Livrare Gratuita</h4>
                    <p >Livrare gratuită pentru comenzile peste 200 de lei. Comandă produsele bio de care ai nevoie și beneficiezi
                        de livrare gratuită, oriunde te-ai afla in oraș.
                    </p>
                </div>
                <div className="sd">
                    <i className="bi bi-truck sd-icon"></i>
                    <h4>Calitate garantată</h4>
                    <p >Colaborăm doar cu producători si fermieri locali, astfel încât să putem garanta calitatea si prospețimea
                        produselor bio pe care le livrăm.
                    </p>
                </div>
                <div className="sd">
                    <i className="bi bi-tree sd-icon"></i>
                    <h4>Susținem mediul inconjurator</h4>
                    <p >Suntem preocupați de mediul înconjurător si susținem producătorii locali, astfel încât să putem oferi
                        produse bio de calitate și să reducem impactul asupra mediului.
                    </p>
                </div>
            </div>
        </>
    )
}

export default Home;
