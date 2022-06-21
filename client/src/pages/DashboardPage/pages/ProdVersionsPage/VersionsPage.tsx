import { useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import React, { createContext, useEffect, useState } from 'react';
import { delProdVer, getProdVersionList, initialProdVerState, setProdVersionDataState } from '../../../../stores/productVersion/reducer';
import { Button } from 'react-bootstrap';
import { Grid, GridColumn, GridDataStateChangeEvent, GridItemChangeEvent, GridNoRecords, GridToolbar } from '@progress/kendo-react-grid';
import CreateVersion from './modals/CreateVersion';
import { IGridDataState } from '../../../../types/common';
import useProdVersion from '../../../../helpers/hooks/storeHooks/useProdVersion';
import { IProductVersion, IProductVersionList } from '../../../../types/productVersion';
import { DateCell } from '../../../../components/grid-components/DateCell';
import VersionsActionCell from './components/VersionsActionCell';
import useProduct from '../../../../helpers/hooks/storeHooks/useProduct';
import { getProductById } from '../../../../stores/product/reducer';
import { Loader } from '@progress/kendo-react-indicators';
import GridLoader from '../../../../components/Loaders/GridLoader/GridLoader';

export const ProdVersionContext = createContext<{
  productId: string|undefined,
  onEdit: (dataItem: IProductVersion) => void,
  onDelete: (dataItem: IProductVersion) => void,
}>({} as any);
const VersionsPage = () => {
  const dispatch = useDispatch()
  const {productId} = useParams();
  const {product, isProductLoading} = useProduct()

  const {prodVersionList, dataState, isProdVersionListLoading} = useProdVersion()
  const [isShowCreateVersionModal, setIsShowCreateVersionModal] = useState(false)
  const [dataToUpdateId, setDataToUpdateId] = useState<string>('')
  const [gridData, setGridData] = useState<IProductVersionList[]>([])
  const lineLoader = <Loader type="pulsing" size="small"/>

  useEffect(() => {
    dispatch(getProductById(productId ?? ''))
    dispatch(getProdVersionList(productId ?? ''))
    return () => {
      dispatch(setProdVersionDataState(initialProdVerState.dataState))
    }
  }, [])
  useEffect(() => {
    setGridData(prodVersionList?.data ?? [])
  }, [prodVersionList])

  const onCreateVersionClick = () => {
    setIsShowCreateVersionModal(!isShowCreateVersionModal)
  }
  const onCreateVerClose = () => {
    setIsShowCreateVersionModal(!isShowCreateVersionModal)
    setDataToUpdateId('')
  }
  const onDataStateChange = (e: GridDataStateChangeEvent) => {
    dispatch(setProdVersionDataState(e.dataState as any))
    dispatch(getProdVersionList(productId ?? ''))
  }
  const itemChange = (event: GridItemChangeEvent) => {
    const field = event.field || '';
    const newData = gridData.map(item =>
      item.id === event.dataItem.id
        ? {...item, [field]: event.value}
        : item
    );
    setGridData(newData)
  };
  const onEdit = (data: IProductVersion) => {
    setIsShowCreateVersionModal(true)
    setDataToUpdateId(data.id)
  }
  const onDelete = (data: IProductVersion) => dispatch(delProdVer(data))

  return (
    <div>
      <div className="version-info-section">
        <h6>Product Name: {!isProductLoading? product?.name: lineLoader}</h6>
        <h6>Product Description: {!isProductLoading? product?.description: lineLoader}</h6>
      </div>
      <div className="version-table">
        <ProdVersionContext.Provider value={{productId, onDelete, onEdit}}>
          <Grid
            className="product-grid version-grid"
            data={gridData}
            {...(dataState as IGridDataState)}
            total={prodVersionList?.total}
            pageable={true}
            sortable={true}
            onItemChange={itemChange}
            onDataStateChange={onDataStateChange}
          >
            <GridToolbar>
              <Button variant="primary" onClick={onCreateVersionClick}>
                Create Version
              </Button>
            </GridToolbar>
            <GridNoRecords>
              <GridLoader isLoading={isProdVersionListLoading}/>
            </GridNoRecords>
            <GridColumn field="name" title="Name"/>
            <GridColumn field="sequenceNumber" title="Sequence Number"/>
            <GridColumn field="version" title="Version"/>
            <GridColumn field="inventorVersion" title="Inventor Version"/>
            <GridColumn field="designGearVersion" title="DesignGear Version"/>
            <GridColumn field="created" title="Date created" cell={DateCell}/>
            <GridColumn fild="action" title="Action" className="action-cell" cell={VersionsActionCell} sortable={false}/>
          </Grid>
        </ProdVersionContext.Provider>
      </div>
      {
        isShowCreateVersionModal && productId &&
        <CreateVersion isOpen={isShowCreateVersionModal} onClose={onCreateVerClose} productId={productId} dataToUpdateId={dataToUpdateId}/>
      }
    </div>
  )
}

export default VersionsPage