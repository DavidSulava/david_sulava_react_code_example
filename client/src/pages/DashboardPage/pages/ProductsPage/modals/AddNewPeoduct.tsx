import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../components/form-components/helpers/validation-functions';
import CTextArea from '../../../../../components/form-components/CTextArea';
import { Button } from 'react-bootstrap';
import React, { useEffect, useRef } from 'react';
import { ICreateNewProductModalProps, IModalWrapperButton } from '../../../../../types/modal';
import { IPostProduct } from '../../../../../types/product';
import { postProduct } from '../../../../../stores/product/reducer';
import { useDispatch, useSelector } from 'react-redux';
import { IState } from '../../../../../stores/configureStore';
import useProduct from '../../../../../helpers/hooks/storeHooks/useProduct';
import { setPostReqResp } from '../../../../../stores/common/reducer';
import Spinner from '../../../../../components/Loaders/Spinner/Spinner';

const AddNewProduct: React.FC<ICreateNewProductModalProps> = ({
  isOpen,
  onClose,
}) => {
  const dispatch = useDispatch()
  const {isProductLoading} = useProduct()
  const backEndResp = useSelector((state: IState) => state.common.postReqResp)
  const formRef = useRef<Form|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)

  const headerText = 'Add product';
  const formState = {}
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: "close", onButtonClick: () => onClose()},
    {buttonText: "save", onButtonClick: () => formSubmitBtnRef?.current?.click(), buttonDisabled:isProductLoading}
  ]

  useEffect(()=>{
    if(backEndResp){
      onClose()
      dispatch(setPostReqResp(''))
    }
  },[backEndResp, isProductLoading])
  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    dispatch(postProduct(formData as IPostProduct))
  }

  return(
    <ModalWrapper
      headerText={headerText}
      isOpen={isOpen}
      onClose={onClose}
      buttons={modalButtons}
      className="create-product-modal"
    >
      <Form
        onSubmit={onSubmitLocal}
        initialValues={formState}
        ref={formRef}
        render={(formRenderProps) => (
          <FormElement className={'modal-body'}>
            {
              isProductLoading? <Spinner/> :
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
            }

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
