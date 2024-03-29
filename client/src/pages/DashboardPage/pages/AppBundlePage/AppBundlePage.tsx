import useAppBundle from '../../../../helpers/hooks/storeHooks/useAppBundle';
import { useDispatch, useSelector } from 'react-redux';
import React, { useEffect, useState } from 'react';
import { IAppBundle, IOnDeleteBundlePayload } from '../../../../types/appBundle';
import {
  Grid,
  GridCellProps,
  GridColumn,
  GridDataStateChangeEvent,
  GridItemChangeEvent,
  GridNoRecords,
  GridToolbar
} from '@progress/kendo-react-grid';
import {
  deleteAppBundle,
  getAppBundleTableList,
  initialAppBundleState,
  setAppBundleDataState,
  setAppBundleTableList
} from '../../../../stores/appBundle/reducer';
import { IGridDataState } from '../../../../types/common';
import { Button } from 'react-bootstrap';
import AppBundleActionCell from './components/AppBundleActionCell';
import CreateAppBundleModal from './modals/CreateAppBundleModal';
import GridLoader from '../../../../components/Loaders/GridLoader/GridLoader';

const AppBundlePage = () => {
  const dispatch = useDispatch()
  const {isBundleListLoading, appBundleTableList, appBundleFilters} = useAppBundle()
  const [isShowAddBundleModal, setIsShowAddBundleModal] = useState(false)
  const [bundleIdToEdit, setBundleIdToEdit] = useState('');
  const [dataState, setDataState] = useState<IAppBundle[]>([]);

  useEffect(() => {
    dispatch(getAppBundleTableList())
    return () => {
      dispatch(setAppBundleTableList(null))
      dispatch(setAppBundleDataState(initialAppBundleState.dataState))
    }
  }, [dispatch])
  useEffect(() => {
    setDataState(appBundleTableList?.data ?? [])
  }, [appBundleTableList])

  const onDataStateChange = (e: GridDataStateChangeEvent) => {
    dispatch(setAppBundleDataState(e.dataState as any))
    dispatch(getAppBundleTableList())
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
  const toggleAppBundleModal = () => {
    setIsShowAddBundleModal(!isShowAddBundleModal)
    setBundleIdToEdit('')
  }
  const onDelete = (item: IAppBundle) => {
    const payload = {id: item.id, name: item.name} as IOnDeleteBundlePayload
    window.confirm(`Are you sure you want to delete:  ${item.name} ?`) &&
    dispatch(deleteAppBundle(payload))
  }
  const onEdit = (item: IAppBundle) => {
    setBundleIdToEdit(item.id)
    setIsShowAddBundleModal(true)
  }

  return (
    <div className="product-page app-bundle-page">
      <Grid
        className="product-grid app-bundle-grid"
        data={dataState}
        {...(appBundleFilters as IGridDataState)}
        total={appBundleTableList?.total}
        pageable={true}
        sortable={true}
        onItemChange={itemChange}
        onDataStateChange={onDataStateChange}
      >
        <GridToolbar>
          <Button variant="primary" onClick={toggleAppBundleModal}>
            ADD
          </Button>
        </GridToolbar>
        <GridNoRecords>
          <GridLoader isLoading={isBundleListLoading}/>
        </GridNoRecords>
        <GridColumn field="name" title="Bundle name"/>
        <GridColumn field="description" title="Short description"/>
        <GridColumn field="designGearVersion" title="Version" sortable={false} editable={false}/>
        <GridColumn field="inventorVersion" title="Inventor version" sortable={false} editable={false}/>
        <GridColumn
          fild="action"
          title="Action"
          className="action-cell"
          sortable={false}
          cell={(props: GridCellProps) =>
            <AppBundleActionCell onDelete={onDelete} onEdit={onEdit} {...props}/>
          }
        />
      </Grid>
      <CreateAppBundleModal isOpen={isShowAddBundleModal} onClose={toggleAppBundleModal} dataToUpdateId={bundleIdToEdit}/>
    </div>
  )
}

export default AppBundlePage