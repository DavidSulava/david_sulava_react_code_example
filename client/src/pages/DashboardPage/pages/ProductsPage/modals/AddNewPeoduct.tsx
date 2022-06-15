import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../components/form-components/helpers/validation-functions';
import CTextArea from '../../../../../components/form-components/CTextArea';
import { Button } from 'react-bootstrap';
import React, { useRef } from 'react';
import { ICreateNewProductModalProps, IModalWrapperButton } from '../../../../../types/modal';
import { IPostProduct } from '../../../../../types/product';
import { postProduct } from '../../../../../stores/product/reducer';
import { useDispatch } from 'react-redux';

const AddNewProduct: React.FC<ICreateNewProductModalProps> = ({
  isOpen,
  onSubmit,
  onClose,
}) => {
  const dispatch = useDispatch()
  const formRef = useRef<Form|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)

  const headerText = 'Add product';
  const formState = {}
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: "close", onButtonClick: () => onClose()},
    {buttonText: "save", onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    dispatch(postProduct(formData as IPostProduct))

    if(onSubmit) onSubmit()
    onClose()
  }

  return(
    <ModalWrapper
      headerText={headerText}
      isOpen={isOpen}
      onClose={onClose}
      buttons={modalButtons}
      className="create-organisation-modal"
    >
      <Form
        onSubmit={onSubmitLocal}
        initialValues={formState}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement>
            <fieldset className={"k-form-fieldset"}>
              <div className="mb-3">
                <Field
                  name={"name"}
                  component={CInput}
                  label={"Please enter a name for your product"}
                  validator={isEmpty}
                />
              </div>

              <div className="mb-3">
                <Field
                  name={"description"}
                  value={formRenderProps.valueGetter("description")}
                  label={"Please share a short description"}
                  component={CTextArea}
                  max={200}
                  cols={50}
                  validator={isEmpty}
                />
              </div>
            </fieldset>
            <Button
              type="submit"
              hidden={true}
              ref={formSubmitBtnRef}
            >
              submit
            </Button>
          </FormElement>
        )}
      />
    </ModalWrapper>
  )
}

export default AddNewProduct
