import useConfigurations from '../../../../../helpers/hooks/useConfigurations';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import {
  getConfigurationList,
  initialConfigurationsState,
  setConfigurationsDataState
} from '../../../../../stores/productConfigurations/reducer';
import { Link, useParams } from 'react-router-dom';
import {
  Grid,
  GridCellProps,
  GridColumn,
  GridDataStateChangeEvent,
  GridItemChangeEvent,
  GridNoRecords,
} from '@progress/kendo-react-grid';
import { IGridDataState, IStdEnum } from '../../../../../types/common';
import { Button } from 'react-bootstrap';
import NoRecords from '../../../../../components/grid-components/NoRecords';
import { DateCell } from '../../../../../components/grid-components/DateCell';
import { enumGetKey } from '../../../../../helpers/enumFunctions';
import { EConfigurationStatus, ESvfStatus, IConfigurationListItem } from '../../../../../types/producVersionConfigurations';
import setPath from '../../../../../helpers/setPath';
import { ERoutes } from '../../../../../router/Routes';

const EnumToCell = (props: GridCellProps, enumObj: IStdEnum) => {
  const field = props.field || ""
  const dateString = enumGetKey(props.dataItem[field], enumObj)

  return (
    <td>{dateString}</td>
  )
}
const ActionsConfigCell = (props: GridCellProps) => {
  const dataItem = {...props.dataItem as  IConfigurationListItem}
  const {organizationId, productId, versionId} = useParams();
  return (
    <td
      className={props.className}
      colSpan={props.colSpan}
      role="gridCell"
      aria-colindex={props.ariaColumnIndex}
    >
      <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0 btn-with-link'>
        <Link to={setPath(ERoutes.ProdVersionConfig, [organizationId, productId, versionId, dataItem.id])}>Open</Link>
      </Button>
    </td>
  )
}

const ConfigurationsTable = () => {
  const dispatch = useDispatch()
  const {isConfigLoading, configurationsList, dataState} = useConfigurations()
  const {versionId} = useParams();
  const [gridData, setGridData] = useState<IConfigurationListItem[]>([])

  useEffect(() => {
    if(versionId)
      dispatch(getConfigurationList(versionId))
    return () => {
      dispatch(setConfigurationsDataState(initialConfigurationsState.dataState))
    }
  }, [])
  useEffect(() => {
    setGridData(configurationsList?.data ?? [])
  }, [configurationsList])
  const itemChange = (event: GridItemChangeEvent) => {
    const field = event.field || '';
    const newData = gridData.map(item =>
      item.id === event.dataItem.id
        ? {...item, [field]: event.value}
        : item
    );
    setGridData(newData)
  };
  const onDataStateChange = (e: GridDataStateChangeEvent) => {
    dispatch(setConfigurationsDataState(e.dataState as any))
    dispatch(getConfigurationList(versionId ?? ''))
  }

  return (
    <div className="configurations-grid-component">
      <Grid
        className="configurations-grid"
        data={gridData}
        {...(dataState as IGridDataState)}
        total={configurationsList?.total}
        pageable={true}
        sortable={true}
        onItemChange={itemChange}
        onDataStateChange={onDataStateChange}
      >
        <GridNoRecords>
          <NoRecords isLoading={isConfigLoading}/>
        </GridNoRecords>
        <GridColumn field="configurationName" title="Name"/>
        <GridColumn field="componentName" title="Component name"/>
        <GridColumn field="status" title="Status" cell={(props: GridCellProps) => EnumToCell(props, EConfigurationStatus)}/>
        <GridColumn field="svfStatus" title="SVF status" cell={(props: GridCellProps) => EnumToCell(props, ESvfStatus)}/>
        <GridColumn field="created" title="Created" cell={DateCell}/>
        <GridColumn field="action" title="Action" sortable={false} className="conf-table-actions" cell={ActionsConfigCell}/>
      </Grid>
    </div>
  )
}

export default ConfigurationsTable