import { useParams } from 'react-router-dom';

const VersionPage = () => {
  const { productId } = useParams();
  return(
    <div>
      <div>product version page:</div>
      <div>Product id: {productId}</div>
    </div>
  )
}

export default VersionPage