import { Field, Form, FormElement } from '@progress/kendo-react-form';
import { Button } from 'react-bootstrap';
import CInput from '../../components/form-components/CInput';
import { isEmpty } from '../../components/form-components/helpers/valodation-functions';
import { ISignInData } from '../../types/LoginPage';
import {useDispatch} from 'react-redux'
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import { Link } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';
import React from 'react';
import { signIn } from '../../stores/authentication/reducer';

const LoginPage = () => {
  const dispatch = useDispatch()
  const {checkingAuth} = useAuthCheck()

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
            </fieldset>
            <div className="k-form-buttons k-flex k-justify-content-center k-mb-5 k-mt-2">
              <Button
                type="submit"
                variant="info"
                disabled={!formRenderProps.allowSubmit || checkingAuth}
              >
                {checkingAuth ? 'Loading...' : 'Sign In'}
              </Button>
            </div>
          </FormElement>
        )}
      />
      <div>Wish create an account?</div>
      <br/>
      <Button variant="outlined" className="btn-outline-info mb-1"  disabled={checkingAuth}>
        <Link to={ERoutes.SignUp}>Sign Up</Link>
      </Button>
      <Button variant="outlined" className="mb-1 btn-outline-info" disabled={checkingAuth}>
        <Link to={ERoutes.ForgotPwd}>Forgot Password?</Link>
      </Button>
    </div>
  )
}

export default LoginPage
