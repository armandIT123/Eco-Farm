import Carousel from 'react-bootstrap/Carousel';

function Slider({ images }) {
  return (
    <div className="centered-carousel">
      <Carousel data-bs-theme="dark">

        {images.map((image, index) => (
          <Carousel.Item key={index}>
            <img
              className="d-block w-100"
              src={image}
              alt=""
            />
          </Carousel.Item>
        ))}
      </Carousel>
    </div>

  );
}

export default Slider;