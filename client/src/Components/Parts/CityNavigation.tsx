import React, { useEffect, useState } from 'react';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Button from '@material-ui/core/Button';
import Grid from '@material-ui/core/Grid';
import Dialog from '@material-ui/core/Dialog';
import DialogTitle from '@material-ui/core/DialogTitle';
import DialogContent from '@material-ui/core/DialogContent';
import AccordionSummary from '@material-ui/core/AccordionSummary';
import Typography from '@material-ui/core/Typography';
import AccordionDetails from '@material-ui/core/AccordionDetails';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import List from '@material-ui/core/List';
import ListItem from '@material-ui/core/ListItem';
import Divider from '@material-ui/core/Divider';
import { Accordion } from '@material-ui/core';

import City from 'src/Types/City';
import Region from 'src/Types/Region';
import { getRegions } from 'src/Requests/GetRequests';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    bar: {
      backgroundColor: 'white',
    },
  }),
);

const CityNavigation: React.FC = () => {
  const classes = useStyles();

  const [city, setCity] = useState<City | null>(null);
  const [regions, setRegions] = useState<Region[]>([]);
  const [open, setOpen] = useState(false);

  useEffect(() => {
    let isMounted = true;
    getCity(isMounted);
    getRegionsInfo(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const getCity = async (isMounted: boolean) => {
    const cityName = localStorage.getItem('cityName');
    const cityId = localStorage.getItem('cityId');
    if (isMounted) {
      if (cityName && cityId) {
        setCity({ id: parseInt(cityId), name: cityName });
      } else {
        setOpen(true);
      }
    }
  };

  const getRegionsInfo = async (isMounted: boolean) => {
    const res = await getRegions();
    if (isMounted) {
      setRegions(res);
    }
  };

  const handleClose = () => {
    const cityName = localStorage.getItem('cityName');
    const cityId = localStorage.getItem('cityId');
    if (!cityName || !cityId) {
      const region = regions.find(r => r.cities.find(c => c.name === 'Москва'));
      if (region) {
        const cityAutoPicked = region.cities.find(c => c.name === 'Москва');
        if (cityAutoPicked) {
          localStorage.setItem('cityName', cityAutoPicked.name);
          localStorage.setItem('cityId', cityAutoPicked.id.toString());
          setCity(cityAutoPicked);
        }
      }
    }
    setOpen(false);
  };

  const onCityClick = (event: React.MouseEvent<HTMLButtonElement>) => {
    const id = parseInt(event.currentTarget.id);
    const reg = regions.find(r => r.cities.find(c => c.id === id));
    const cit = reg?.cities.find(c => c.id === id);
    if (cit) {
      pickCity(cit);
    }
  };

  const pickCity = (cit: City) => {
    localStorage.setItem('cityName', cit.name);
    localStorage.setItem('cityId', cit.id.toString());
    setCity(cit);
    setOpen(false);
  };

  const handleClickOpen = () => {
    setOpen(true);
  };

  return (
    <Grid item xs={12} className={classes.bar}>
      <Button variant="text" size="small" color="primary" onClick={handleClickOpen}>
        {city?.name ?? 'Выберите город'}
      </Button>
      <Dialog
        open={open}
        onClose={handleClose}
        aria-labelledby="alert-dialog-title"
        aria-describedby="alert-dialog-description"
      >
        <DialogTitle id="alert-dialog-title">Выберите город</DialogTitle>
        <DialogContent>
          {regions.map((reg, index) => (
            <Accordion key={index}>
              <AccordionSummary expandIcon={<ExpandMoreIcon />} aria-controls="panel1a-content" id="panel1a-header">
                <Typography>{reg.name}</Typography>
              </AccordionSummary>
              <AccordionDetails>
                <Grid item xs={12}>
                  <List component="nav">
                    {reg.cities.map((cit, ind) => (
                      <React.Fragment key={ind}>
                        <Divider />
                        <ListItem>
                          <Button
                            id={cit.id.toString()}
                            onClick={onCityClick}
                            variant="text"
                            size="small"
                            color="primary"
                          >
                            {cit.name}
                          </Button>
                        </ListItem>
                      </React.Fragment>
                    ))}
                  </List>
                </Grid>
              </AccordionDetails>
            </Accordion>
          ))}
        </DialogContent>
      </Dialog>
    </Grid>
  );
};
export default CityNavigation;
