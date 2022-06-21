import { useDispatch, useSelector } from 'react-redux';
import useAuthCheck from '../../../helpers/hooks/storeHooks/useAuthCheck';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../../components/form-components/CInput';
import { emailValidator, isEmpty, passwordValidation } from '../../../components/form-components/helpers/validation-functions';
import { Button } from 'react-bootstrap';
import React, { useEffect, useRef } from 'react';
import { ERoutes } from '../../../router/Routes';
import { EUserRoles, ISignUpData } from '../../../types/user';
import { signUp } from '../../../stores/authentication/reducer';
import { setError, setPostReqResp } from '../../../stores/common/reducer';
import { IState } from '../../../stores/configureStore';
import { Error } from '@progress/kendo-react-labels';
import BtnLink from '../../../components/BtnLink';
import { ICommonObject } from '../../../types/common';

const RegistrationPage = () => {
  const dispatch = useDispatch()
  const {authLoading} = useAuthCheck()
  const postResp = useSelector((state: IState) => state.common.postReqResp)
  const error = useSelector((state: IState) => state.common.error)

  const formRef = useRef<Form>(null)

  useEffect(() => {
    dispatch(setPostReqResp(''))
    return () => {
      dispatch(setPostReqResp(''))
      dispatch(setError(''))
    }
  }, [dispatch])

  const handleSubmit = (dataItem: ICommonObject) => {
    const postData = {...dataItem}
    delete postData?.confirmPassword
    postData.phone = ''
    postData.role = EUserRoles.User

    dispatch(signUp(postData as ISignUpData))
  }

  return (
    <div className="sign-in-form">
      {
        postResp ?
          <>
            <div>
              The User successfully created, please log in!
            </div>
            <br/>
          </>
          :
          // Form
          <>
            <div>
              <legend className={"k-form-legend mb-1"}>
                Please enter your details
              </legend>
            </div>
            <Form
              onSubmit={handleSubmit}
              ref={formRef}
              validator={(val)=>passwordValidation(val)}
              render={(formRenderProps) => (
                <FormElement>
                  <fieldset className={"k-form-fieldset"}>
                    <div>
                      <Field
                        name={"email"}
                        type={"email"}
                        component={CInput}
                        label={"Email"}
                        maxLength={300}
                        validator={emailValidator}
                      />
                      {
                        !!error && formRenderProps.valid && <Error>{error}</Error>
                      }
                    </div>

                    <div>
                      <Field
                        name={"firstName"}
                        component={CInput}
                        label={"First name"}
                        validator={isEmpty}
                      />
                    </div>

                    <div>
                      <Field
                        name={"lastName"}
                        component={CInput}
                        label={"Second name"}
                        validator={isEmpty}
                      />
                    </div>

                    <div>
                      <Field
                        name={"password"}
                        component={CInput}
                        label={"Password"}
                        type="password"
                      />
                    </div>

                    <div className="mb-2">
                      <Field
                        name={"confirmPassword"}
                        component={CInput}
                        label={"Confirm password"}
                        type="password"
                      />
                    </div>
                  </fieldset>
                  <div className="k-form-buttons k-flex k-justify-content-center k-mb-3 k-mt-2">
                    <Button
                      type="submit"
                      variant="primary"
                      disabled={!formRenderProps.allowSubmit || authLoading}
                    >
                      {authLoading ? 'Loading...' : 'Sign Up'}
                    </Button>
                  </div>
                </FormElement>
              )}
            />
          </>
      }
      {
        !postResp &&
        <div className="mb-2">Already have an account?</div>
      }
      <BtnLink to={ERoutes.Root} className="btn btn-outline-primary mb-1" idDisabled={authLoading}>Sign in</BtnLink>
      {
        !postResp &&
        <BtnLink to={ERoutes.ForgotPwd} idDisabled={authLoading} className='btn btn-outline-primary mb-1'>Forgot Password?</BtnLink>
      }
    </div>
  )
}

export default RegistrationPage