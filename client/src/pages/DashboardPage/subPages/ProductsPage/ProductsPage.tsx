import {
  Grid,
  GridCellProps,
  GridColumn,
  GridDataStateChangeEvent,
  GridItemChangeEvent,
  GridRowProps,
  GridToolbar
} from '@progress/kendo-react-grid';
import React, { createContext, ReactElement, useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import { delProduct, getProduct, putProduct, setFilter, setProduct } from '../../../../stores/product/reducer';
import useProduct from '../../../../helpers/hooks/useProduct';
import { IGridDataState, IProduct, IPutProduct } from '../../../../types/product';
import { Button } from 'react-bootstrap';
import { useParams } from 'react-router-dom';
import AddNewProduct from './modals/AddNewPeoduct';
import { MyEditCell } from './components/MyEditCell';
import { GridInlineFormRow } from './components/GridInlineFormRow';
import { FormCell, IFormCellProps } from './components/FormCell';

export const GridEditContext = createContext<{
  cancel: (dataItem: IGridState) => void,
  enterEdit: (dataItem: IGridState) => void,
  update: (dataItem: IGridState) => void,
  onDelete: (dataItem: IGridState) => void,
}>({} as any);

const ActionCell = (props: GridCellProps) => {
  return (
    <MyEditCell
      {...props}
    />
  )
};
const RowRender = (row: ReactElement<HTMLTableRowElement>, props: GridRowProps) => {
  return (<GridInlineFormRow dataItem={props.dataItem}>{row}</GridInlineFormRow>);
};

export interface IGridState extends IProduct {
  inEdit?: boolean
}
const ProductsPage = () => {
  const dispatch = useDispatch()
  const {organizationId} = useParams();
  const {product, productFilters} = useProduct()
  const editField = "inEdit";
  const [isShowAddProductModal, setIsShowAddProductModal] = useState(false)
  const [dataState, setDataState] = useState<IGridState[]>([]);

  useEffect(() => {
    dispatch(setProduct([]))
    dispatch(getProduct(organizationId ?? ''))
    setDataState(product)
  }, [dispatch])
  useEffect(() => {
    setDataState(product)
  }, [product])

  const onAddProdClick = () => {
    setIsShowAddProductModal(!isShowAddProductModal)
  }
  const onDataStateChange = (e: GridDataStateChangeEvent) => {
    dispatch(setFilter(e.dataState as any))
    dispatch(getProduct(organizationId ?? ''))
  }
  // modify the data in the store, db etc
  const onDelete = (dataItem: IGridState) => {
    const sendData = {
      prodId: dataItem.id,
      organisationId: dataItem.organizationId
    }
    dispatch(delProduct(sendData))
  };
  const update = (dataItem: IGridState) => {
    cancel(dataItem)
    const updatedData = {
      id: dataItem.id,
      name: dataItem.name,
      description: dataItem.description,
      organizationId: dataItem.organizationId
    }
    if(updatedData.name && updatedData.description)
      dispatch(putProduct(updatedData as IPutProduct))
  };
  // Local state operations
  const cancel = (dataItem: IGridState) => {
    const originalItem = product.find(
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
  const enterEdit = (dataItem: IGridState) => {
    let newData = dataState.map(item =>
      item.id === dataItem.id ? {...item, inEdit: true} : item
    )
    setDataState(newData);
  };
  const itemChange = (event: GridItemChangeEvent) => {
    const field = event.field || '';
    const newData = dataState.map(item =>
      item.id === event.dataItem.id
        ? {...item, [field]: event.value}
        : item
    );
    setDataState(newData)
  };

  return (
    <div className="product-page">
      <GridEditContext.Provider value={{cancel, enterEdit, update, onDelete}}>
        <Grid
          className="product-grid"
          data={dataState}
          {...(productFilters as IGridDataState)}
          total={dataState.length}
          pageable={true}
          sortable={true}
          editField={editField}
          onItemChange={itemChange}
          onDataStateChange={onDataStateChange}
          rowRender={RowRender}
        >
          <GridToolbar>
            <Button variant="primary" onClick={onAddProdClick}>
              ADD
            </Button>
          </GridToolbar>
          <GridColumn field="name" title="Product name" className="grid-cell-form" cell={FormCell}/>
          <GridColumn
            field="description"
            title="Short description"
            className="grid-cell-form"
            cell={(props: IFormCellProps) => <FormCell {...props} textArea={true} rows={2}/>}
          />
          <GridColumn field="created" title="Date added" sortable={false} editable={false}/>
          <GridColumn
            field="currentVersion"
            title="Version"
            sortable={false}
            editable={false}
          />
          <GridColumn fild="action" title="Action" cell={ActionCell}/>
        </Grid>
      </GridEditContext.Provider>
      {
        isShowAddProductModal &&
        <AddNewProduct
          isOpen={isShowAddProductModal}
          organizationId={organizationId ?? ''}
          onClose={onAddProdClick}
        />
      }
    </div>
  )
}
export default ProductsPage