import {
  Grid,
  GridCellProps,
  GridColumn,
  GridDataStateChangeEvent,
  GridItemChangeEvent, GridNoRecords,
  GridRowProps,
  GridToolbar
} from '@progress/kendo-react-grid';
import React, { createContext, ReactElement, useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { delProduct, getProductList, putProduct, setProductFilter, setProductList, initialProductState } from '../../../../stores/product/reducer';
import useProduct from '../../../../helpers/hooks/storeHooks/useProduct';
import { IProduct, IPutProduct } from '../../../../types/product';
import { Button } from 'react-bootstrap';
import AddNewProduct from './modals/AddNewPeoduct';
import { ProductsActionCell } from './components/ProductsActionCell';
import { GridInlineFormRow } from './components/GridInlineFormRow';
import { FormCell, IFormCellProps } from './components/FormCell';
import { IGridDataState } from '../../../../types/common';
import GridLoader from '../../../../components/Loaders/GridLoader/GridLoader';
import { DateCell } from '../../../../components/grid-components/DateCell';

export const GridEditContext = createContext<{
  cancel: (dataItem: IGridProductData) => void,
  enterEdit: (dataItem: IGridProductData) => void,
  update: (dataItem: IGridProductData) => void,
  onDelete: (dataItem: IGridProductData) => void,
}>({} as any);

const ActionCell = (props: GridCellProps) => {
  return (
    <ProductsActionCell
      {...props}
    />
  )
};
const RowRender = (row: ReactElement<HTMLTableRowElement>, props: GridRowProps) => {
  return (<GridInlineFormRow dataItem={props.dataItem}>{row}</GridInlineFormRow>);
};

export interface IGridProductData extends IProduct{
  inEdit?: boolean
}
const ProductsPage = () => {
  const dispatch = useDispatch()
  const {productList, productFilters, isProductListLoading} = useProduct()
  const [isShowAddProductModal, setIsShowAddProductModal] = useState(false)
  const [dataState, setDataState] = useState<IGridProductData[]>([]);

  useEffect(() => {
    dispatch(getProductList())
    return() => {
      dispatch(setProductList(null))
      dispatch(setProductFilter(initialProductState.dataState))
    }
  }, [dispatch])
  useEffect(() => {
    setDataState(productList?.data ?? [])
  }, [productList])

  const onAddProdClick = () => {
    setIsShowAddProductModal(!isShowAddProductModal)
  }
  const onDataStateChange = (e: GridDataStateChangeEvent) => {
    dispatch(setProductFilter(e.dataState as any))
    dispatch(getProductList())
  }
  const itemChange = (event: GridItemChangeEvent) => {
    const field = event.field || '';
    const newData = dataState.map(item =>
      item.id === event.dataItem.id
        ? {...item, [field]: event.value}
        : item
    );
    setDataState(newData)
  };
  // modify the data in the store, db etc
  const onDelete = (dataItem: IGridProductData) => {
    const sendData = {
      prodId: dataItem.id,
      organisationId: dataItem.organizationId
    }
    dispatch(delProduct(sendData))
  };
  const update = (dataItem: IGridProductData) => {
    cancel(dataItem)
    const updatedData = {
      id: dataItem.id,
      name: dataItem.name,
      description: dataItem.description
    }
    if(updatedData.name && updatedData.description)
      dispatch(putProduct(updatedData as IPutProduct))
  };
  // Local state operations
  const cancel = (dataItem: IGridProductData) => {
    const originalItem = productList?.data.find(
      p => p.id === dataItem.id
    );
    setDataState((prev) => {
      return prev.map(item => {
        if(originalItem && item.id === originalItem.id)
          return {...originalItem, inEdit: false}
        return item
      })
    });
  };
  const enterEdit = (dataItem: IGridProductData) => {
    let newData = dataState.map(item =>
      item.id === dataItem.id ? {...item, inEdit: true} : item
    )
    setDataState(newData);
  };

  return (
    <div className="product-page">
      <GridEditContext.Provider value={{cancel, enterEdit, update, onDelete}}>
        <Grid
          className="product-grid"
          data={dataState}
          {...(productFilters as IGridDataState)}
          total={productList?.total}
          pageable={true}
          sortable={true}
          onItemChange={itemChange}
          onDataStateChange={onDataStateChange}
          rowRender={RowRender}
        >
          <GridToolbar>
            <Button variant="primary" onClick={onAddProdClick}>
              ADD
            </Button>
          </GridToolbar>
          <GridNoRecords>
            <GridLoader isLoading={isProductListLoading}/>
          </GridNoRecords>
          <GridColumn field="name" title="Product name" cell={FormCell} className="grid-cell-form"/>
          <GridColumn
            field="description"
            title="Short description"
            cell={(props: IFormCellProps) => <FormCell {...props} textArea={true} rows={2}/>}
          />
          <GridColumn field="created" title="Date added" sortable={false} cell={DateCell}/>
          <GridColumn
            field="currentVersion"
            title="Version"
            sortable={false}
            editable={false}
          />
          <GridColumn fild="action" title="Action"  cell={ActionCell}/>
        </Grid>
      </GridEditContext.Provider>
      {
        isShowAddProductModal &&
        <AddNewProduct
          isOpen={isShowAddProductModal}
          onClose={onAddProdClick}
        />
      }
    </div>
  )
}
export default ProductsPage