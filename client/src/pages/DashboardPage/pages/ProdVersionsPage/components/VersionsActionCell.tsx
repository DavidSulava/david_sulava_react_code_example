import { GridCellProps } from '@progress/kendo-react-grid';
import React, { useContext } from 'react';
import { ProdVersionContext } from '../VersionsPage';
import { Button } from 'react-bootstrap';
import { Link } from 'react-router-dom';
import setPath from '../../../../../helpers/setPath';
import { ERoutes } from '../../../../../router/Routes';
import { IProduct } from '../../../../../types/product';

const VersionsActionCell = (props: GridCellProps) => {
  const {dataItem} = props
  const {onEdit, onDelete, productId} = useContext(ProdVersionContext)

  return (
    <td
      className={props.className}
      colSpan={props.colSpan}
      role="gridcell"
      aria-colindex={props.ariaColumnIndex}
      aria-selected={props.isSelected}
    >
      <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0 btn-with-link'>
        <Link to={setPath(ERoutes.ProdVersion, [productId, dataItem.productId, dataItem.id])} state={{...dataItem as IProduct}}>Show</Link>
      </Button>
      <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={() => onEdit(dataItem)}>
        Edit
      </Button>
      <Button
        variant="outline-secondary"
        className='k-m-1 pt-0 pb-0'
        onClick={() =>
          window.confirm(`Are you sure you want to delete:  ${dataItem.name} ?`) &&
          onDelete(dataItem)
        }
      >
        Delete
      </Button>
    </td>
  )
}
export default VersionsActionCell