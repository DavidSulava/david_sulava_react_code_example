import { useSelector } from 'react-redux';
import { IState } from '../../../stores/configureStore';

const useProduct= () => {
  const productFilters = useSelector((state: IState) => state.product.dataState)
  const productList = useSelector((state: IState) => state.product.productList)
  const product = useSelector((state: IState) => state.product.product)
  const isProductListLoading = useSelector((state: IState) => state.product.isProductListLoading)
  const isProductLoading = useSelector((state: IState) => state.product.isProductLoading)

  return {
    productFilters,
    productList,
    product,
    isProductListLoading,
    isProductLoading
  }
}

export default useProduct