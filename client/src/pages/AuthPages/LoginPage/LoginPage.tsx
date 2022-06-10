import { Field, Form, FormElement } from '@progress/kendo-react-form';
import { Button } from 'react-bootstrap';
import CInput from '../../../components/form-components/CInput';
import { isEmpty } from '../../../components/form-components/helpers/validation-functions';
import { useDispatch, useSelector } from 'react-redux'
import useAuthCheck from '../../../helpers/hooks/useAuthCheck';
import { ERoutes } from '../../../router/Routes';
import React, { useEffect } from 'react';
import { signIn } from '../../../stores/authentication/reducer';
import { ISignInData } from '../../../types/user';
import BtnLink from '../../../components/BtnLink';
import { IState } from '../../../stores/configureStore';
import { Error } from '@progress/kendo-react-labels';
import { setError } from '../../../stores/common/reducer';

const LoginPage = () => {
  const dispatch = useDispatch()
  const {authLoading} = useAuthCheck()
  const error = useSelector((state: IState) => state.common.error)

  useEffect(()=>{
    return ()=>{
      dispatch(setError(''))
    }
  },[])
  const handleSubmit = (dataItem: {[p:string]: any}) => {
    const postData = {...dataItem} as ISignInData
    dispatch(signIn(postData))
  }

  return (
    <div className="sign-in-form">
      <div>
        <legend className={"k-form-legend"}>
          Please fill in your credentials
        </legend>
      </div>
      <Form
        onSubmit={handleSubmit}
        render={(formRenderProps) => (
          <FormElement>
            <fieldset className={"k-form-fieldset"}>
              <div className="mb-3">
                <Field
                  name={"email"}
                  // type={"email"}
                  component={CInput}
                  label={"Email"}
                  // validator={emailValidator}
                  maxLength={300}
                  validator={isEmpty}
                />
              </div>

              <div className="mb-3">
                <Field
                  name={"password"}
                  component={CInput}
                  label={"Password"}
                  type="password"
                  validator={isEmpty}
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
                {authLoading ? 'Loading...' : 'Sign In'}
              </Button>
            </div>
          </FormElement>
        )}
      />
      <div>Would you like to create an account?</div>
      <br/>
      <BtnLink to={ERoutes.SignUp} idDisabled={authLoading} className='btn btn-outline-primary mb-1'>Sign Up</BtnLink>
      <BtnLink to={ERoutes.ForgotPwd} idDisabled={authLoading} className='btn btn-outline-primary mb-1'>Forgot Password?</BtnLink>
      <br/>
    </div>
  )
}

export default LoginPage
