import { ERoutes } from '../../../router/Routes';
import React, { useEffect, useState } from 'react';
import BtnLink from '../../../components/BtnLink';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../components/form-components/CInput';
import { emailValidator } from '../../../components/form-components/helpers/validation-functions';
import { Error } from '@progress/kendo-react-labels';
import { Button } from 'react-bootstrap';
import { useDispatch, useSelector } from 'react-redux';
import { IState } from '../../../stores/configureStore';
import useAuthCheck from '../../../helpers/hooks/storeHooks/useAuthCheck';
import { setError, setPostReqResp } from '../../../stores/common/reducer';
import { IPostPasswordRecovery } from '../../../types/user';
import { postSendEmailToRestorePassword } from '../../../stores/authentication/reducer';

const ForgotPassword = () => {
  const dispatch = useDispatch()
  const {authLoading} = useAuthCheck()
  const postResp = useSelector((state: IState) => state.common.postReqResp)
  const error = useSelector((state: IState) => state.common.error)
  const [isEmailSent, setIsEmailSent] = useState(false)

  useEffect(() => {
    return () => {
      dispatch(setError(''))
    }
  }, [])
  useEffect(() => {
    if(postResp) {
      setIsEmailSent(true)
      dispatch(setPostReqResp(''))
    }
  }, [postResp])

  const onSubmit = (formData: any) => {
    const postData = {...formData} as IPostPasswordRecovery
    dispatch(postSendEmailToRestorePassword(postData))
  }

  return (
    <div className="sign-in-form forgot-pwd-form">
      {
        isEmailSent ?
          <div>
            <b>We have sent a link to your email, to reset your password.</b>
          </div>
          :
          <>
            <div>
              <legend className={"k-form-legend"}>
                Please enter your email
              </legend>
            </div>
            <Form
              onSubmit={onSubmit}
              render={(formRenderProps) => (
                <FormElement>
                  <fieldset className={"k-form-fieldset"}>
                    <div className="mb-3">
                      <Field
                        name={"email"}
                        type={"email"}
                        component={CInput}
                        label={"Email"}
                        validator={emailValidator}
                        maxLength={300}
                      />
                    </div>

                    {
                      //TODO: Display an error message sent from the backend, when 400 will be implemented
                      !!error && formRenderProps.valid &&
                      <div className="k-d-flex k-justify-content-center"><Error>{'Something went wrong'}</Error></div>
                    }
                  </fieldset>
                  <div className="k-form-buttons k-flex k-justify-content-center k-mb-5 k-mt-2">
                    <Button
                      type="submit"
                      variant="primary"
                      disabled={!formRenderProps.allowSubmit || authLoading}
                    >
                      {authLoading ? 'Loading...' : 'Send'}
                    </Button>
                  </div>
                </FormElement>
              )}
            />
          </>
      }

      <br/>
      <BtnLink to={ERoutes.Root} className="btn btn-outline-primary mb-1" idDisabled={authLoading}>Sign in</BtnLink>
      <BtnLink to={ERoutes.SignUp} idDisabled={authLoading} className='btn btn-outline-primary mb-1'>Sign Up</BtnLink>
      <br/>
    </div>
  )
}

export default ForgotPassword