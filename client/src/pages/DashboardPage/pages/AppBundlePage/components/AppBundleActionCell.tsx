import { GridCellProps } from '@progress/kendo-react-grid';
import React, { FC, useEffect } from 'react';
import { Button } from 'react-bootstrap';
import { IAppBundle } from '../../../../../types/appBundle';
import { useSelector } from 'react-redux';
import { IState } from '../../../../../stores/configureStore';
import { Loader } from '@progress/kendo-react-indicators';

interface IAppBundleActionCellProps extends GridCellProps {
  onEdit: (dataItem: IAppBundle) => void,
  onDelete: (dataItem: IAppBundle) => void,
  dataItem: IAppBundle
}

const AppBundleActionCell: FC<IAppBundleActionCellProps> = ({
  dataItem,
  onEdit,
  onDelete,
  ...others
}) => {
  const inPendingList = useSelector((state: IState) => state.common.inPendingList)
  const isInPending = inPendingList.includes(dataItem?.id)

  return (
    <td
      className={others.className}
      colSpan={others.colSpan}
      role="gridcell"
      aria-colindex={others.ariaColumnIndex}
      aria-selected={others.isSelected}
    >
      <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={() => onEdit(dataItem)} disabled={isInPending}>
        Edit
      </Button>
      <Button
        variant="outline-secondary"
        className='k-m-1 pt-0 pb-0'
        onClick={() => onDelete(dataItem)}
        disabled={isInPending}
      >
        {isInPending? <Loader type="pulsing" size="small"/> : 'Delete'}
      </Button>
    </td>
  )
}

export default AppBundleActionCell