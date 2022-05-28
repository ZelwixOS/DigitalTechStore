import React from 'react';
import { Typography, Card, Grid } from '@material-ui/core';

import RegistrationForm from 'src/Components/Parts/RegistrationForm';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import Register from 'src/Types/Register';

interface IRegistration {
  regData?: Register;
}

const RegistrationPage: React.FC<IRegistration> = () => (
  <React.Fragment>
    <script src="https://apis.google.com/js/platform.js" async defer />
    <NavigationBar />
    <Grid container justifyContent="center" alignItems="center">
      <Grid xs={12} sm={9} item direction="column" justifyContent="center" alignItems="center" container>
        <Card style={{ padding: '35px' }} variant="outlined">
          <Typography variant="h4" style={{ paddingBottom: '10px' }}>
            Регистрация
          </Typography>
          <RegistrationForm regData={history.state} />
        </Card>
      </Grid>
    </Grid>
  </React.Fragment>
);

export default RegistrationPage;
