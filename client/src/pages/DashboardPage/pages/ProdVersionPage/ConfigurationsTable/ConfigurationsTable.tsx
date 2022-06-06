import useConfigurations from '../../../../../helpers/hooks/useConfigurations';
import React, { useEffect, useState } from 'react';
import { useDispatch } from 'react-redux';
import {
  getConfigurations,
  initialConfigurationsState,
  setConfigurationsDataState
} from '../../../../../stores/productConfigurations/reducer';
import { useParams } from 'react-router-dom';
import {
  Grid,
  GridCellProps,
  GridColumn,
  GridDataStateChangeEvent,
  GridItemChangeEvent,
  GridNoRecords,
  GridToolbar
} from '@progress/kendo-react-grid';
import { IGridDataState, IStdEnum } from '../../../../../types/common';
import { Button } from 'react-bootstrap';
import NoRecords from '../../../../../components/grid-components/NoRecords';
import CreateConfigModal from '../CreateConfigModal/CreateConfigModal';
import { DateCell } from '../../../../../components/grid-components/DateCell';
import { enumGetKey } from '../../../../../helpers/enumFunctions';
import { EConfigurationStatus, ESvfStatus, IConfigurations } from '../../../../../types/producVersionConfigurations';
import { getProdVersionList, setProdVersionDataState } from '../../../../../stores/productVersion/reducer';
import { IProductVersion } from '../../../../../types/productVersion';

const enumToCell = (props: GridCellProps, enumObj: IStdEnum) => {
  const field = props.field || ""
  const dateString = enumGetKey(props.dataItem[field], enumObj)

  return (
    <td>{dateString}</td>
  )
}

const ConfigurationsTable = () => {
  const dispatch = useDispatch()
  const {isConfigLoading, configurationsList, dataState} = useConfigurations()
  const {versionId} = useParams();
  const [isCreateFormOpen, setIsCreateFormOpen] = useState(false)
  const [gridData, setGridData] = useState<IConfigurations[]>([])

  useEffect(() => {
    if(versionId)
      dispatch(getConfigurations(versionId))
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
    dispatch(getConfigurations(versionId ?? ''))
  }

  const onPressCreate = () => {
    setIsCreateFormOpen(!isCreateFormOpen)
  }
  return (
    <div className="configurations-grid-component">
      <Grid
        className="configurations-grid version-grid"
        data={gridData}
        {...(dataState as IGridDataState)}
        total={configurationsList?.total}
        pageable={true}
        sortable={true}
        onItemChange={itemChange}
        onDataStateChange={onDataStateChange}
      >
        <GridToolbar>
          <Button variant="primary" onClick={onPressCreate}  disabled={false}>
            Create Configuration
          </Button>
          <Button variant="primary" disabled={true}>
            Generate SVF
          </Button>
        </GridToolbar>
        <GridNoRecords>
          <NoRecords isLoading={isConfigLoading}/>
        </GridNoRecords>
        <GridColumn field="configurationName" title="Name"/>
        <GridColumn field="componentName" title="Component name"/>
        <GridColumn field="status" title="Status" cell={(props: GridCellProps) => enumToCell(props, EConfigurationStatus)}/>
        <GridColumn field="svfStatus" title="SVF status" cell={(props: GridCellProps) => enumToCell(props, ESvfStatus)}/>
        <GridColumn field="created" title="Created" cell={DateCell}/>
      </Grid>
      <CreateConfigModal isOpen={isCreateFormOpen} onClose={onPressCreate}/>
    </div>
  )
}

export default ConfigurationsTable