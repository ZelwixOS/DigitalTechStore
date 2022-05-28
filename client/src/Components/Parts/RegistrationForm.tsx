import React from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Grid, TextField } from '@material-ui/core';

import { registrationRequest, regViaGoogleRequest, serverAnswers } from 'src/Requests/AccountRequests';
import ServerResponse from 'src/Types/ServerResponse';
import Register from 'src/Types/Register';

import ErrorSnackBar from './ErrorSnackBar';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      marginTop: theme.spacing(1),
    },
    button: {
      marginTop: theme.spacing(3),
    },
  }),
);

interface IRegistrationForm {
  regData?: Register;
}

const RegistrationForm: React.FC<IRegistrationForm> = props => {
  const classes = useStyles();

  const newObject = (oldData: Register): Register => ({
    login: oldData.login,
    password: oldData.password,
    firstName: oldData.firstName,
    secondName: oldData.secondName,
    phoneNumber: oldData.phoneNumber,
    token: oldData.token,
    email: oldData.email,
  });

  const [errors, setErrors] = React.useState<string[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const [rerendered, setRererendered] = React.useState(false);
  const [regData, setRegData] = React.useState<Register>({
    login: '',
    password: '',
    firstName: '',
    secondName: '',
    email: '',
    phoneNumber: '',
    token: '',
  });

  if (props.regData && !rerendered) {
    setRegData(props.regData);
    setRererendered(true);
  }

  const handleLoginChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.login = event.target.value as string;
    setRegData(newData);
  };

  const handlePasswordChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.password = event.target.value as string;
    setRegData(newData);
  };

  const handleEMailChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.email = event.target.value as string;
    setRegData(newData);
  };

  const handleFirstNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.firstName = event.target.value as string;
    setRegData(newData);
  };

  const handleSecondNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.secondName = event.target.value as string;
    setRegData(newData);
  };

  const handlePhoneChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newData = newObject(regData);
    newData.phoneNumber = event.target.value as string;
    setRegData(newData);
  };

  const registerButtonClicked = async () => {
    if (!props.regData?.token) {
      redirectTo(await registrationRequest(regData));
    } else {
      redirectTo(await regViaGoogleRequest(regData));
    }
  };

  const redirectTo = (response: ServerResponse) => {
    switch (response.code) {
      case serverAnswers.signedIn:
        window.location.replace('/');
        break;
      case serverAnswers.noCommand:
        setMessage(response.message);
        setErrors(response.errors);
        setOpen(true);
        break;
    }
  };

  return (
    <Grid container direction="column" justifyContent="center">
      <TextField
        id="login-tf"
        className={classes.spaces}
        value={regData.login}
        onChange={handleLoginChange}
        label="Логин"
        variant="outlined"
      />
      <TextField
        id="password-tf"
        className={classes.spaces}
        type="password"
        value={regData.password}
        onChange={handlePasswordChange}
        label="Пароль"
        variant="outlined"
      />
      <TextField
        id="email-tf"
        className={classes.spaces}
        value={regData.email}
        onChange={handleEMailChange}
        label="Email"
        variant="outlined"
      />
      <TextField
        id="first-name-tf"
        className={classes.spaces}
        value={regData.firstName}
        onChange={handleFirstNameChange}
        label="Имя"
        variant="outlined"
      />
      <TextField
        id="second-name-tf"
        className={classes.spaces}
        value={regData.secondName}
        onChange={handleSecondNameChange}
        label="Фамилия"
        variant="outlined"
      />
      <TextField
        id="phone-tf"
        className={classes.spaces}
        value={regData.phoneNumber}
        onChange={handlePhoneChange}
        label="Номер телефона"
        variant="outlined"
      />
      <Button
        type="submit"
        className={classes.button}
        color="primary"
        variant="contained"
        onClick={registerButtonClicked}
      >
        Зарегистрироваться
      </Button>
      <ErrorSnackBar message={message} errors={errors} open={open} setOpen={setOpen} />
    </Grid>
  );
};

export default RegistrationForm;
