import { GridCellProps } from '@progress/kendo-react-grid';
import React, { FC } from 'react';
import { Button } from 'react-bootstrap';
import { IAppBundle } from '../../../../../types/appBundle';

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

  return (
    <td
      className={others.className}
      colSpan={others.colSpan}
      role="gridcell"
      aria-colindex={others.ariaColumnIndex}
      aria-selected={others.isSelected}
    >
      <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={() => onEdit(dataItem)}>
        Edit
      </Button>
      <Button
        variant="outline-secondary"
        className='k-m-1 pt-0 pb-0'
        onClick={() => onDelete(dataItem)}
      >
        Delete
      </Button>
    </td>
  )
}

export default AppBundleActionCell