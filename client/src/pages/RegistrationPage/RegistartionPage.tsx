import { useDispatch } from 'react-redux';
import useAuthCheck from '../../helpers/hooks/useAuthCheck';
import { Field, Form, FormElement } from '@progress/kendo-react-form';
import CInput from '../../components/form-components/CInput';
import { isEmpty } from '../../components/form-components/helpers/valodation-functions';
import { Button } from 'react-bootstrap';
import React from 'react';
import { Link } from 'react-router-dom';
import { ERoutes } from '../../router/Routes';

const RegistrationPage = () => {
  const dispatch = useDispatch()
  const {checkingAuth} = useAuthCheck()

  const handleSubmit = (dataItem: {[p:string]: any}) => {
    // const postData = {...dataItem} as ISignInData
    // dispatch(AuthenticationActions.signIn(postData))
  }

  return (
    <div className="sign-in-form">
      <div>
        <legend className={"k-form-legend mb-1"}>
          Please enter your details
        </legend>
      </div>
      <Form
        onSubmit={handleSubmit}
        render={(formRenderProps) => (
          <FormElement>
            <fieldset className={"k-form-fieldset"}>
              <div>
                <Field
                  name={"email"}
                  // type={"email"}
                  component={CInput}
                  label={"Email"}
                  // validator={emailValidator}
                  validator={isEmpty}
                />
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
                  name={"secondName"}
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
                  validator={isEmpty}
                />
              </div>

              <div className="mb-2">
                <Field
                  name={"confirmPassword"}
                  component={CInput}
                  label={"Confirm password"}
                  type="password"
                  validator={isEmpty}
                />
              </div>
            </fieldset>
            <div className="k-form-buttons k-flex k-justify-content-center k-mb-3 k-mt-2">
              <Button
                type="submit"
                variant="info"
                disabled={!formRenderProps.allowSubmit || checkingAuth}
              >
                {checkingAuth ? 'Loading...' : 'Sign Up'}
              </Button>
            </div>
          </FormElement>
        )}
      />

      <div className="mb-2">Already have an account?</div>
      <Button variant="outlined" className="btn-outline-info mb-1">
        <Link to={ERoutes.Root}>Sign in</Link>
      </Button>
      <Button variant="outlined" className="mb-1 btn-outline-info">
        <Link to={ERoutes.ForgotPwd}>Forgot Password?</Link>
      </Button>
    </div>
  )
}

export default RegistrationPage