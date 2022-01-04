import React from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Checkbox, FormControlLabel, Grid, TextField, Typography } from '@material-ui/core';
import GoogleLogin, { GoogleLoginResponse } from 'react-google-login';

import Login from 'src/Types/Login';
import { authViaGoogle, logInRequest, serverAnswers } from 'src/Requests/AccountRequests';
import ServerResponse from 'src/Types/ServerResponse';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      marginTop: theme.spacing(1),
    },
  }),
);

const LoginForm: React.FC = () => {
  const classes = useStyles();

  const [loginData, setLoginData] = React.useState<Login>({ login: '', password: '', rememberMe: true });

  const handleRememberMeChange = () => {
    setLoginData({ login: loginData.login, password: loginData.password, rememberMe: !loginData.rememberMe });
  };

  const handleLoginChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setLoginData({
      login: event.target.value as string,
      password: loginData.password,
      rememberMe: loginData.rememberMe,
    });
  };

  const handlePasswordChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    setLoginData({
      login: loginData.login,
      password: event.target.value as string,
      rememberMe: loginData.rememberMe,
    });
  };

  const handleGoogleLoginSuccess = async (googleData: unknown) => {
    if (googleData) {
      const token = (googleData as GoogleLoginResponse)?.getAuthResponse()?.id_token;
      if (token) {
        redirectTo(await authViaGoogle(token));
      }
    }
  };

  const logInButtonClicked = async () => {
    redirectTo(await logInRequest(loginData));
  };

  const redirectTo = (response: ServerResponse) => {
    switch (response.code) {
      case serverAnswers.goToGoogleRegistrationPage:
        window.location.replace('/Registration');
        break;
      case serverAnswers.signedIn:
        window.location.replace('/');
        break;
    }
  };

  return (
    <Grid container direction="column" justify="center">
      <TextField
        id="login-tf"
        className={classes.spaces}
        value={loginData.login}
        onChange={handleLoginChange}
        label="Login"
        variant="outlined"
      />
      <TextField
        id="password-tf"
        className={classes.spaces}
        type="password"
        value={loginData.password}
        onChange={handlePasswordChange}
        label="Password"
        variant="outlined"
      />
      <FormControlLabel
        control={
          <Checkbox
            checked={loginData.rememberMe}
            onChange={handleRememberMeChange}
            name="rememberMe"
            color="primary"
          />
        }
        label="Remember me"
        className={classes.spaces}
      />
      <Grid container justify="flex-end">
        <Button
          type="submit"
          className={classes.spaces}
          color="primary"
          variant="contained"
          onClick={logInButtonClicked}
        >
          LogIn
        </Button>
      </Grid>
      <Grid className={classes.spaces} container justify="center">
        <Typography>Sign in with other services</Typography>
      </Grid>
      <Grid container justify="center">
        <GoogleLogin
          clientId={'23242950767-kd98v0m5fodo8a7npg7kod07hj1fs97k.apps.googleusercontent.com'}
          buttonText="Log in with Google"
          onSuccess={handleGoogleLoginSuccess}
          cookiePolicy={'single_host_origin'}
        />
      </Grid>
    </Grid>
  );
};

export default LoginForm;
