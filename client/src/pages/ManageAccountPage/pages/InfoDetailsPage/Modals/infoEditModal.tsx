import React, { FC, useEffect, useRef, useState } from 'react';
import { ICommonModalProps } from '../../../../../types/common';
import { useDispatch } from 'react-redux';
import { IModalWrapperButton } from '../../../../../types/modal';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import useAuthCheck from '../../../../../helpers/hooks/useAuthCheck';
import ModalWrapper from '../../../../../components/ModalWrapper/ModalWrapper';
import CInput from '../../../../../components/form-components/CInput';
import { isEmpty } from '../../../../../components/form-components/helpers/validation-functions';
import { Button } from 'react-bootstrap';
import { putAccountInfo } from '../../../../../stores/authentication/reducer';

const InfoEditModal: FC<ICommonModalProps> = ({isOpen, onClose}) => {
  const dispatch = useDispatch()
  const headerText = 'User info edit';
  const {account} = useAuthCheck()
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)
  const [formState, setFormState] = useState({})
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: 'close', onButtonClick: () => onClose()},
    {buttonText: 'save', onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  useEffect(()=>{
    if(account)
      setFormState({
        firstName: account.firstName,
        lastName: account.lastName,
        phone: account.phone,
      })
  }, [account])

  const onSubmitLocal = (formData: any)=>{
    const data = {
      firstName: formData?.firstName,
      lastName: formData?.lastName,
      phone: formData?.phone,
    };

    dispatch(putAccountInfo(data))

    onClose()
  }

  return(
    <>
      <ModalWrapper
        headerText={headerText}
        isOpen={isOpen}
        onClose={onClose}
        buttons={modalButtons}
      >
        <Form
          onSubmit={onSubmitLocal}
          initialValues={formState}
          key={JSON.stringify(formState)}
          render={(formRenderProps) => (
            <FormElement>
              <fieldset className="k-form-fieldset">
                <div className="mb-3">
                  <Field
                    name={"firstName"}
                    component={CInput}
                    label={"Name"}
                    validator={isEmpty}
                  />
                </div>
                <div className="mb-3">
                  <Field
                    name={"lastName"}
                    label={"Last name"}
                    component={CInput}
                    validator={isEmpty}
                  />
                </div>
                <div className="mb-3">
                  <div className="mb-3">
                    <Field
                      name={"phone"}
                      label={"Phone"}
                      component={CInput}
                      validator={isEmpty}
                    />
                  </div>
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
    </>
  )
}

export default InfoEditModal