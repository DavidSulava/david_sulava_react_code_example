import { useLocation, useParams } from 'react-router-dom';
import { useDispatch } from 'react-redux';
import React, { createContext, useEffect, useState } from 'react';
import { delProdVer, getProdVersionList, setProdVersionDataState } from '../../../../stores/productVersion/reducer';
import { Button } from 'react-bootstrap';
import { Grid, GridColumn, GridDataStateChangeEvent, GridItemChangeEvent, GridNoRecords, GridToolbar } from '@progress/kendo-react-grid';
import CreateVersion from './modals/CreateVersion';
import { IGridDataState } from '../../../../types/common';
import useProdVersion from '../../../../helpers/hooks/useProdVersion';
import { IProductVersion } from '../../../../types/productVersion';
import { DateCell } from '../../../../components/grid-components/DateCell';
import NoRecords from '../../../../components/grid-components/NoRecords';
import { IProduct } from '../../../../types/product';
import VersionsActionCell from './components/VersionsActionCell';

export const ProdVersionContext = createContext<{
  productId: string| undefined,
  onEdit: (dataItem: IProductVersion) => void,
  onDelete: (dataItem: IProductVersion) => void,
}>({} as any);
const VersionsPage = () => {
  const dispatch = useDispatch()
  const {productId} = useParams();
  const {state} = useLocation();
  const product = state as IProduct

  const {prodVersionList, dataState, isProdVersionLoading} = useProdVersion()
  const [isShowCreateVersionModal, setIsShowCreateVersionModal] = useState(false)
  const [dataToUpdate, setDataToUpdate] = useState<IProductVersion>()
  const [gridData, setGridData] = useState<IProductVersion[]>([])

  useEffect(() => {
    dispatch(getProdVersionList(productId ?? ''))
  }, [])
  useEffect(() => {
    setGridData(prodVersionList?.data ?? [])
  }, [prodVersionList])

  const onCreateVersionClick = () => {
    setIsShowCreateVersionModal(!isShowCreateVersionModal)
    setDataToUpdate(undefined)
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
    setDataToUpdate(data)
  }
  const onDelete = (data: IProductVersion) => dispatch(delProdVer(data))

  return (
    <div>
      <div className="version-info-section">
        <h6>Product Name: {product?.name}</h6>
        <h6>Product Description: {product?.description}</h6>
      </div>
      <div className="version-table">
        <ProdVersionContext.Provider value={{productId, onDelete, onEdit}}>
          <Grid
            className="version-grid"
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
              <NoRecords isLoading={isProdVersionLoading}/>
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
        <CreateVersion isOpen={isShowCreateVersionModal} onClose={onCreateVersionClick} productId={productId} dataToUpdate={dataToUpdate}/>
      }
    </div>
  )
}

export default VersionsPage