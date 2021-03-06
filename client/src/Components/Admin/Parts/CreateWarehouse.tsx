import React, { useEffect } from 'react';
import Button from '@material-ui/core/Button';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { CircularProgress, Grid, MenuItem, Snackbar, TextField } from '@material-ui/core';
import { Alert } from '@mui/material';

import { createWarehouse } from 'src/Requests/PostRequests';
import { getAllRegions } from 'src/Requests/GetRequests';
import Region from 'src/Types/Region';
import City from 'src/Types/City';

interface IRefresher {
  refresh: () => void;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    spaces: {
      margin: theme.spacing(2),
    },
  }),
);

interface ICreateWarehouse {
  refresher?: IRefresher;
  setOpen: React.Dispatch<React.SetStateAction<boolean>>;
}

const CreateWarehouse: React.FC<ICreateWarehouse> = props => {
  const classes = useStyles();

  const getData = async (isMounted: boolean) => {
    setLoading(true);
    const regions = await getAllRegions();
    if (isMounted) {
      setRegions(regions);
      setLoading(false);
    }
  };

  const refreshData = () => {
    let isMounted = true;
    getData(isMounted);

    return () => {
      isMounted = false;
    };
  };

  useEffect(() => {
    refreshData();
  }, []);

  const cloneData = () => ({
    name: outletData.name,
    cityId: outletData.cityId,
    streetName: outletData.streetName,
    building: outletData.building,
    postalCode: outletData.postalCode,
    phoneNumber: outletData.phoneNumber,
  });

  const [outletData, setOutletData] = React.useState({
    name: '',
    cityId: 0,
    streetName: '',
    building: '',
    postalCode: '',
    phoneNumber: '',
  });

  const [loading, setLoading] = React.useState(true);
  const [regions, setRegions] = React.useState<Region[]>([]);
  const [region, setRegion] = React.useState(0);
  const [cities, setCities] = React.useState<City[]>([]);
  const [open, setOpen] = React.useState<boolean>(false);
  const [message, setMessage] = React.useState<string>('');

  const handleNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.name = event.target.value as string;
    setOutletData(data);
  };

  const handleStreetNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.streetName = event.target.value as string;
    setOutletData(data);
  };

  const handleBuildingChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.building = event.target.value as string;
    setOutletData(data);
  };

  const handlePostalCodeChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.postalCode = event.target.value as string;
    setOutletData(data);
  };

  const handlePhoneChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.phoneNumber = event.target.value as string;
    setOutletData(data);
  };

  const handleRegionChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const regionId = event.target.value as number;
    setRegion(regionId);
    const reg = regions.find(r => r.id === regionId);
    if (reg) {
      setCities(reg.cities);
    }
  };

  const handleCityChange = async (event: React.ChangeEvent<{ value: unknown }>) => {
    const data = cloneData();
    data.cityId = event.target.value as number;
    setOutletData(data);
  };

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const onClick = async () => {
    try {
      const res = await createWarehouse(
        outletData.name,
        outletData.cityId,
        outletData.streetName,
        outletData.building,
        outletData.postalCode,
        outletData.phoneNumber,
      );

      if (res && props.refresher) {
        props.refresher.refresh();
        props.setOpen(false);
      } else {
        setMessage('???? ?????????????? ?????????????????? ????????????');
        setOpen(true);
      }
    } catch (e) {
      setMessage(`???? ?????????????? ?????????????????? ????????????: ${e}`);
      setOpen(true);
    }
  };

  return (
    <Grid container direction="column" justifyContent="center">
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="warning">
          {message}
        </Alert>
      </Snackbar>
      {loading ? (
        <Grid container alignContent="stretch" justifyContent="center">
          <CircularProgress />
        </Grid>
      ) : (
        <React.Fragment>
          <Grid container direction="column" justifyContent="space-evenly">
            <TextField
              id="outletName"
              className={classes.spaces}
              value={outletData.name}
              onChange={handleNameChange}
              label="????????????????"
              variant="outlined"
            />
            <TextField
              id="outletPhone"
              className={classes.spaces}
              value={outletData.phoneNumber}
              onChange={handlePhoneChange}
              label="?????????? ????????????????"
              variant="outlined"
            />
            <TextField
              id="outletPostalCode"
              className={classes.spaces}
              value={outletData.postalCode}
              onChange={handlePostalCodeChange}
              label="???????????????? ??????"
              variant="outlined"
            />
            <TextField
              id="region"
              select
              label="????????????"
              className={classes.spaces}
              value={region}
              onChange={handleRegionChange}
              variant="outlined"
            >
              {regions.map(p => (
                <MenuItem key={p.id} value={p.id}>
                  {p.name}
                </MenuItem>
              ))}
            </TextField>
            {region !== 0 && (
              <React.Fragment>
                <TextField
                  id="city"
                  select
                  label="??????????"
                  className={classes.spaces}
                  value={outletData.cityId}
                  onChange={handleCityChange}
                  variant="outlined"
                >
                  {cities.map(p => (
                    <MenuItem key={p.id} value={p.id}>
                      {p.name}
                    </MenuItem>
                  ))}
                </TextField>
                <TextField
                  id="street"
                  className={classes.spaces}
                  value={outletData.streetName}
                  onChange={handleStreetNameChange}
                  label="??????????"
                  variant="outlined"
                />
                <TextField
                  id="building"
                  className={classes.spaces}
                  value={outletData.building}
                  onChange={handleBuildingChange}
                  label="????????????"
                  variant="outlined"
                />
              </React.Fragment>
            )}
          </Grid>
          <Grid container justifyContent="flex-end">
            <Button type="submit" className={classes.spaces} color="primary" variant="contained" onClick={onClick}>
              ??????????????
            </Button>
          </Grid>
        </React.Fragment>
      )}
    </Grid>
  );
};

export default CreateWarehouse;
