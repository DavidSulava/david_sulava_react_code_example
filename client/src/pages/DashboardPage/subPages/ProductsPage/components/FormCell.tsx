import { GridCellProps } from '@progress/kendo-react-grid';
import { Field } from '@progress/kendo-react-form';
import CInput from '../../../../../components/form-components/CInput';
import { isEmptyNoMsg } from '../../../../../components/form-components/helpers/valodation-functions';
import React from 'react';
import CTextArea from '../../../../../components/form-components/CTextArea';

export interface IFormCellProps extends GridCellProps {
  textArea?: boolean
  rows?: number
}

export const FormCell = (props: IFormCellProps) => {
  const isInEdit = props.dataItem.inEdit;
  const isTextArea = props.textArea;
  const rows = props.rows ?? 1

  return (
    <td className={props.className}>
      {
        isInEdit ?
          <Field
            component={!isTextArea ? CInput : CTextArea}
            name={`${props.field}`}
            validator={isEmptyNoMsg}
            rows={rows}
            max={isTextArea ? 200 : undefined}
          />
          : props.dataItem[props.field || '']}
    </td>
  );
};