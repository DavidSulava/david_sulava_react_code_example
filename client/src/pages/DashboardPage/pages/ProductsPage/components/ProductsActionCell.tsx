import { GridCellProps } from '@progress/kendo-react-grid';
import { GridEditContext } from '../ProductsPage';
import { Button } from 'react-bootstrap';
import React, { useContext } from 'react';
import { FormSubmitContext } from './GridInlineFormRow';
import { Link, useParams } from 'react-router-dom';
import setPath from '../../../../../helpers/setPath';
import { ERoutes } from '../../../../../router/Routes';
import { IProduct } from '../../../../../types/product';

export const ProductsActionCell = (props: GridCellProps) => {
  const onSubmit = useContext(FormSubmitContext);
  const {enterEdit, cancel, onDelete} = useContext(GridEditContext);
  const {organizationId} = useParams();
  const {dataItem} = props
  const inEdit = dataItem.inEdit;

  const onSaveClick = (e: any) => {
    e.preventDefault();
    onSubmit(e);
  };
  const onEditClick = () => enterEdit(dataItem);
  const onCancelClick = () => cancel(dataItem);
  const onDeleteClick = () => onDelete(dataItem);

  return (
    <td
      className={props.className}
      colSpan={props.colSpan}
      role="gridcell"
      aria-colindex={props.ariaColumnIndex}
      aria-selected={props.isSelected}
    >
      <>
        {
          inEdit ?
            <>
              <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={onSaveClick}>
                Update
              </Button>
              <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={onCancelClick}>
                Cancel
              </Button>
            </>
            :
            <>
              <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0 btn-with-link'>
                <Link to={setPath(ERoutes.ProdVersions, [organizationId, dataItem.id])}>Show</Link>
              </Button>
              <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0' onClick={onEditClick}>
                Edit
              </Button>
              <Button variant="outline-secondary" className='k-m-1 pt-0 pb-0'
                      onClick={() =>
                        window.confirm(`Are you sure you want to delete:  ${dataItem.name} ?`) &&
                        onDeleteClick()
                      }
              >
                Delete
              </Button>
            </>
        }
      </>
    </td>)
};