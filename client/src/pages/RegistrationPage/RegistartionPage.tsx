import { useDispatch, useSelector } from 'react-redux';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../components/form-components/CInput';
import { emailValidator, isEmpty } from '../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import React, { useEffect, useRef } from 'react';
import { ERoutes } from '../../router/Routes';
import { eUserRoles, ISignUpData } from '../../types/user';
import { getter } from '@progress/kendo-data-query';
import { signUp } from '../../stores/authentication/reducer';
import { setPostReqResp, setError } from '../../stores/common/reducer';
import { IState } from '../../stores/configureStore';
import { Error } from '@progress/kendo-react-labels';
import BtnLink from '../../components/BtnLink';

const RegistrationPage = () => {
  const dispatch = useDispatch()
  const {checkingAuth} = useAuthCheck()
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

  const handleSubmit = (dataItem: {[p: string]: any}) => {
    const postData = {...dataItem}
    delete postData?.confirmPassword
    postData.phone = ''
    postData.role = eUserRoles.User

    dispatch(signUp(postData as ISignUpData))
  }
  const passwordValidation = (values: any) => {
    const password: string = getter("password")(values);
    const passConfirm: string = getter("confirmPassword")(values);

    const passRegex: RegExp = new RegExp(/^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])/);

    let msg = ""
    if(password !== passConfirm)
      msg = 'passwords do not match'
    else if(!password?.length || password?.length < 8 || passConfirm?.length < 8)
      msg = 'password must contain at least 8 characters'
    else if(!passRegex.test(password))
      msg = 'The password must contain at least one : uppercase letter, lowercase letter, number.'


    return {
      ["password"]: msg? ' ': '',
      ["confirmPassword"]: msg,
    };
  };

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
              validator={passwordValidation}
              render={(formRenderProps) => (
                <FormElement>
                  <fieldset className={"k-form-fieldset"}>
                    <div>
                      <Field
                        name={"email"}
                        type={"email"}
                        component={CInput}
                        label={"Email"}
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
                      disabled={!formRenderProps.allowSubmit || checkingAuth}
                    >
                      {checkingAuth ? 'Loading...' : 'Sign Up'}
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
      <BtnLink to={ERoutes.Root} className="btn btn-outline-primary mb-1" idDisabled={checkingAuth}>Sign in</BtnLink>
      {
        !postResp &&
        <BtnLink to={ERoutes.ForgotPwd} idDisabled={checkingAuth} className='btn btn-outline-primary mb-1'>Forgot Password?</BtnLink>
      }
    </div>
  )
}

export default RegistrationPage