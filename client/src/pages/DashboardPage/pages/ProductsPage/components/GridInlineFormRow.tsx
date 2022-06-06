import React, { createContext, useCallback, useContext, useEffect, useRef } from 'react';
import { Form } from '@progress/kendo-react-form';
import { GridEditContext, IGridProductData } from '../ProductsPage';

export const FormSubmitContext = createContext<(event: React.SyntheticEvent<any, Event>) => void>(() => undefined);
export const GridInlineFormRow = (props: {children: any; dataItem: IGridProductData}) => {
  const {update} = useContext(GridEditContext);
  const {dataItem} = props;
  const isInEdit = dataItem.inEdit;
  const formRef = useRef<Form>(null)

  useEffect(() => {
    if(formRef.current)
      formRef.current.modified['name'] = true // без этого форма если не была изменена, не будет реагировать на submit
  })

  const onSubmit = useCallback((formData: any) => {
    update(formData as IGridProductData)
  }, [update]);

  if(isInEdit) {
    return (
      <Form
        ref={formRef}
        key={JSON.stringify(props.dataItem)}
        initialValues={props.dataItem}
        onSubmit={onSubmit}
        render={(formRenderProps) => {
          return (<FormSubmitContext.Provider value={formRenderProps.onSubmit}>{props.children}</FormSubmitContext.Provider>);
        }}
      />
    );
  }
  return props.children;
};