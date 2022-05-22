import ModalWrapper from '../../../components/ModalWrapper/ModalWrapper';
import React, { useRef } from 'react';
import { ICreateNewOrganisationModalProps, IModalWrapperButton } from '../../../types/Modal';
import { Field, FieldWrapper, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../components/form-components/CInput';
import { isEmpty } from '../../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import CTextArea from '../../../components/form-components/CTextArea';
import { DropDownList } from '@progress/kendo-react-dropdowns';
import { eOrganisationTypes, IPostOrganisation } from '../../../types/OrganisationPage';
import getEnumKeys from '../../../helpers/getEnumKeys';
import { useDispatch } from 'react-redux';
import useAuthCheck from '../../../helpers/hooks/useAuthCheck';
import { postOrganisation } from '../../../stores/organisation/reducer';

const CreateNewOrganisationModal: React.FC<ICreateNewOrganisationModalProps> = ({
  isOpen,
  onSubmit,
  onClose,
}) => {
  const dispatch = useDispatch()
  const {user} = useAuthCheck()
  const formRef = useRef<Form|null>(null)
  const formSubmitBtnRef = useRef<HTMLButtonElement|null>(null)
  const headerText = 'Create organisation';
  const organizationTypes = getEnumKeys(eOrganisationTypes)
  const formState = {
    "orgType": organizationTypes[0],
  }
  const modalButtons: IModalWrapperButton[] = [
    {buttonText: "close", onButtonClick: () => onClose()},
    {buttonText: "save", onButtonClick: () => formSubmitBtnRef?.current?.click()}
  ]

  const onSubmitLocal = (formData: any) => {
    if(!formRef?.current?.isValid()) return

    const sendData = formData
    sendData.orgType = eOrganisationTypes[sendData.orgType]
    sendData.userId = user?.id

    dispatch(postOrganisation(sendData as IPostOrganisation))

    if(onSubmit) onSubmit()
    onClose()
  }

  return (
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
                  label={"Please enter the name of your organisation"}
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

              <div className="mb-3">
                <div>Type of organization</div>
                <FieldWrapper>
                  <DropDownList
                    name="orgType"
                    style={{width: "50%"}}
                    data={organizationTypes}
                    defaultValue={organizationTypes[0]}
                    onChange={val => formRef?.current?.valueSetter("orgType", val.target.value)}
                  />
                </FieldWrapper>

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
export default CreateNewOrganisationModal