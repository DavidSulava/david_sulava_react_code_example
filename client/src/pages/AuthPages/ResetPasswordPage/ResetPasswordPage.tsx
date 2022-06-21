import { useDispatch, useSelector } from 'react-redux';
import { useParams } from 'react-router-dom';
import useAuthCheck from '../../../helpers/hooks/storeHooks/useAuthCheck';
import { IState } from '../../../stores/configureStore';
import React, { useEffect, useState } from 'react';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../components/form-components/CInput';
import { Error } from '@progress/kendo-react-labels';
import { Button } from 'react-bootstrap';
import BtnLink from '../../../components/BtnLink';
import { ERoutes } from '../../../router/Routes';
import { passwordValidation } from '../../../components/form-components/helpers/validation-functions';
import { postPasswordRecoveryConfirm } from '../../../stores/authentication/reducer';
import { IPostPasswordRecoveryConfirm } from '../../../types/user';
import { setError, setPostReqResp } from '../../../stores/common/reducer';

const ResetPasswordPage = () => {
  const dispatch = useDispatch();
  const {passwordRecoveryKey} = useParams();
  const {authLoading} = useAuthCheck()
  const postResp = useSelector((state: IState) => state.common.postReqResp)
  const error = useSelector((state: IState) => state.common.error)
  const pwdFieldName = 'newPassword'
  const confirmPwdFieldName = 'confirmPassword'
  const [isPasswordSent, setIsPasswordSent] = useState(false)

  useEffect(() => {
    return () => {
      dispatch(setError(''))
    }
  }, [])
  useEffect(() => {
    if(postResp) {
      setIsPasswordSent(true)
      dispatch(setPostReqResp(''))
    }
  }, [postResp])
  const onSubmit = (formData:any)=>{
    const postData = {...formData, passwordRecoveryKey: passwordRecoveryKey}
    dispatch(postPasswordRecoveryConfirm(postData as IPostPasswordRecoveryConfirm))
  }

  return(
    <div className="sign-in-form reset-pwd-form">
      {
        isPasswordSent ?
          <>
            <b>Password has been changed successfully! Please login with the new password.</b>
            <br/>
            <BtnLink to={ERoutes.Root} className="btn btn-outline-primary mb-1" idDisabled={authLoading}>Sign in</BtnLink>
          </>
          :
          <>
            <div>
              <legend className={"k-form-legend"}>
                Please enter a new password
              </legend>
            </div>
            <Form
              onSubmit={onSubmit}
              validator={(val)=>passwordValidation(val, pwdFieldName, confirmPwdFieldName)}
              render={(formRenderProps) => (
                <FormElement>
                  <fieldset className={"k-form-fieldset"}>
                    <div>
                      <Field
                        name={pwdFieldName}
                        component={CInput}
                        label={"New password"}
                        type="password"
                      />
                    </div>

                    <div className="mb-2">
                      <Field
                        name={confirmPwdFieldName}
                        component={CInput}
                        label={"Confirm password"}
                        type="password"
                      />
                    </div>

                    {
                      !!error && formRenderProps.valid &&
                      <div className="k-d-flex k-justify-content-center"><Error>{error}</Error></div>
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
    </div>
  )
}

export default ResetPasswordPage