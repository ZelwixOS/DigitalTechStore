import React, { useEffect, useState } from 'react';
import {
  Typography,
  Card,
  Grid,
  TextField,
  ListItem,
  Divider,
  List,
  RadioGroup,
  Radio,
  FormControlLabel,
  Snackbar,
  Button,
} from '@material-ui/core';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import { Alert } from '@material-ui/lab';

import PurchaseDetailedInfo from 'src/Components/Parts/PurchaseDetailedInfo';
import PurchasingItemsList from 'src/Components/Parts/PurchasingItemsList';
import NavigationBar from 'src/Components/Parts/NavigationBar';
import OutletInfo from 'src/Components/Parts/OutletInfo';
import PrepurchaseInfo from 'src/Types/PrepurchaseInfo';
import ItemOfPurchase from 'src/Types/ItemOfPurchase';
import { createPurchase, getPrepurchaseInfo } from 'src/Requests/PostRequests';
import CustomerInfo from 'src/Types/CustomerInfo';
import PurchaseRequest from 'src/Types/PurchaseRequest';
import Purchase from 'src/Types/Purchase';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    topCard: {
      paddingTop: theme.spacing(5),
    },
    pageName: {
      paddingBottom: theme.spacing(2),
    },
    card: {
      marginTop: theme.spacing(2),
      padding: theme.spacing(2),
    },
    field: {
      margin: theme.spacing(2),
    },
    text: {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
    },
    textSep: {
      marginTop: theme.spacing(3),
    },
  }),
);

const PurchasingPage: React.FC = () => {
  const getPreInfo = async (isMounted: boolean) => {
    const data = parseParams();
    const res = (await getPrepurchaseInfo(data)) as PrepurchaseInfo;
    if (isMounted) {
      setPrepurchaseInfo(res);
      setSum(res.sum);
    }
  };

  useEffect(() => {
    let isMounted = true;
    getPreInfo(isMounted);
    return () => {
      isMounted = false;
    };
  }, []);

  const parseParams = (): ItemOfPurchase[] => {
    const result = [] as ItemOfPurchase[];
    const params = new URLSearchParams(location.search);
    const toParse = params.get('items');
    const arr = toParse?.split(',');
    let dataArr: string[];
    for (const item of arr as string[]) {
      dataArr = item.split('_');
      result.push({ productId: dataArr[0], count: parseInt(dataArr[1] as string) });
    }

    return result;
  };

  const createNewCustomerInfo = (curr: CustomerInfo): CustomerInfo =>
    ({ customerFullName: curr.customerFullName, customerTelephone: curr.customerTelephone } as CustomerInfo);

  const role = sessionStorage.getItem('signed');
  const [prepurchaseInfo, setPrepurchaseInfo] = useState<PrepurchaseInfo>();
  const [customerInfo, setCustomerInfo] = useState<CustomerInfo>({ customerFullName: '', customerTelephone: '' });
  const [deliveryType, setDeliveryType] = useState<string>('outlet');
  const [recieverName, setRecieverName] = useState<string>('');
  const [recieverAdress, setRecieverAdress] = useState<string>('');
  const [outlet, setOutlet] = useState<string>('');
  const [sum, setSum] = useState<number>(0);
  const [operationResult, setResult] = useState<Purchase>();

  const classes = useStyles();

  const nameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newVal = event.target.value as string;
    const newData = createNewCustomerInfo(customerInfo);
    newData.customerFullName = newVal;
    setCustomerInfo(newData);
  };

  const phoneChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const regex = /^\+?[\d ]*$/;
    const newVal = event.target.value as string;
    if (regex.test(newVal)) {
      const newData = createNewCustomerInfo(customerInfo);
      newData.customerTelephone = newVal;
      setCustomerInfo(newData);
    }
  };

  const deliveryTypeChange = (event: React.ChangeEvent<HTMLInputElement>, value: string) => {
    setDeliveryType(value);
    if (prepurchaseInfo) {
      if (value === 'outlet') {
        setSum(prepurchaseInfo.sum);
      } else {
        setSum(prepurchaseInfo.sum + prepurchaseInfo.deliveryPrice);
      }
    }
  };

  const outletChange = (event: React.ChangeEvent<HTMLInputElement>, value: string) => {
    setOutlet(value);
  };

  const recieverNameChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newVal = event.target.value as string;
    setRecieverName(newVal);
  };

  const recieverAdressChange = (event: React.ChangeEvent<{ value: unknown }>) => {
    const newVal = event.target.value as string;
    setRecieverAdress(newVal);
  };

  const [open, setOpen] = React.useState(false);
  const [errors, setErrors] = React.useState<string[]>([]);

  const handleClose = (event?: React.SyntheticEvent, reason?: string) => {
    if (reason === 'clickaway') {
      return;
    }

    setOpen(false);
  };

  const submit = async () => {
    const err = [] as string[];
    if (role && role !== 'Customer') {
      if (customerInfo.customerFullName.length < 3) {
        err.push('?????????????? ???????????????????????? ?????? ????????????????????!');
      }
      if (customerInfo.customerTelephone.length < 8) {
        err.push('???????????? ???????????????????????? ???????????????????? ?????????? ????????????????????!');
      }
    }

    if (deliveryType === 'outlet') {
      if (outlet.length < 1) {
        err.push('???? ???????????? ?????????????? ?????? ????????????????????!');
      }
    } else {
      if (recieverName.length < 3) {
        err.push('?????????????? ???????????????????????? ?????? ????????????????????!');
      }
      if (recieverAdress.length < 5) {
        err.push('???? ???????????? ?????????? ????????????????!');
      }
    }

    if (prepurchaseInfo && prepurchaseInfo.purchaseItems.length < 1) {
      err.push('?????????????????? ????????????: ???? ?????????????? ????????????.');
    }

    if (err.length > 0) {
      setErrors(err);
      setOpen(true);
    } else {
      const data = {
        customerFullName: customerInfo.customerFullName,
        customerTelephone: customerInfo.customerTelephone,
        cityId: 0,
        purchaseItems: (prepurchaseInfo as PrepurchaseInfo).purchaseItems,
      } as PurchaseRequest;

      if (deliveryType === 'outlet') {
        data.deliveryOutletId = parseInt(outlet);
      } else {
        data.delivery = { deliveryAdress: recieverAdress, recieverName: recieverName, cityId: 0 };
      }

      const result = await createPurchase(data);
      if (result) {
        setResult(result as Purchase);
        if (!role) {
          const items = localStorage.getItem('cartItems');
          const itemArr = items?.split(',');
          let index;
          if (prepurchaseInfo && itemArr) {
            for (const prod of prepurchaseInfo.purchaseItems) {
              index = itemArr.findIndex(i => i === prod.productId);
              if (index && index !== -1) {
                itemArr?.splice(index, 1);
              }
            }
            localStorage.setItem('cartItems', itemArr.join(','));
          }
        }
      } else {
        err.push('?????????????????? ???????????????????????????? ????????????!');
        setErrors(err);
        setOpen(true);
      }
    }
  };

  return (
    <React.Fragment>
      <NavigationBar />
      <Snackbar
        anchorOrigin={{ vertical: 'top', horizontal: 'center' }}
        open={open}
        autoHideDuration={6000}
        onClose={handleClose}
      >
        <Alert onClose={handleClose} severity="error">
          <ul>
            {errors.map((e, i) => (
              <li key={i}>{e}</li>
            ))}
          </ul>
        </Alert>
      </Snackbar>
      <Grid container justifyContent="center" alignItems="center">
        <Grid xs={12} sm={9} item direction="column" container>
          {!operationResult && (
            <React.Fragment>
              <Card className={classes.topCard} variant="outlined">
                <Typography align="center" variant="h4" className={classes.pageName}>
                  ???????????????????? ????????????
                </Typography>
              </Card>
              <Card className={classes.card} variant="outlined">
                <Grid xs={12} item justifyContent="center" alignContent="flex-start" container>
                  <Grid xs={12} sm={9} item direction="column" container>
                    <Typography align="center" variant="h5" className={classes.text}>
                      ???????????? ????????????:
                    </Typography>
                    <Card>
                      <List>
                        <Divider />
                        {prepurchaseInfo && <PurchasingItemsList purchaseItems={prepurchaseInfo.purchaseItems} />}
                      </List>
                    </Card>
                    <Typography align="right" variant="h6" className={classes.textSep}>
                      ??????????: {prepurchaseInfo?.sum}???
                    </Typography>
                  </Grid>
                </Grid>
              </Card>
              {role !== 'Customer' && (
                <Card className={classes.card} variant="outlined">
                  <Typography align="center" variant="h5" className={classes.text}>
                    ???????????? ????????????????????:
                  </Typography>
                  <Grid xs={12} item justifyContent="center" container>
                    <Grid xs={12} sm={6} item direction="column" container>
                      <TextField
                        className={classes.field}
                        value={customerInfo.customerFullName}
                        onChange={nameChange}
                        variant="outlined"
                        label="?????? ????????????????????"
                      />
                      <TextField
                        className={classes.field}
                        value={customerInfo.customerTelephone}
                        onChange={phoneChange}
                        variant="outlined"
                        label="?????????? ????????????????"
                      />
                    </Grid>
                  </Grid>
                </Card>
              )}
              <Card className={classes.card} variant="outlined">
                <Typography align="center" variant="h5" className={classes.text}>
                  ?????????? ????????????????:
                </Typography>
                <Grid xs={12} item justifyContent="space-evenly" direction="column" alignItems="center" container>
                  <RadioGroup className={classes.field} row value={deliveryType} onChange={deliveryTypeChange}>
                    <FormControlLabel
                      value="outlet"
                      control={<Radio color="primary" />}
                      label="?????????????????? ???? ????????????????"
                    />
                    <FormControlLabel value="delivery" control={<Radio color="primary" />} label="???????????????? ???? ??????" />
                  </RadioGroup>
                  {deliveryType === 'outlet' ? (
                    <Card>
                      <List>
                        <RadioGroup row value={outlet} onChange={outletChange}>
                          <Divider />
                          {prepurchaseInfo?.outlets.map(o => (
                            <React.Fragment key={o.id}>
                              <ListItem>
                                <Grid alignItems="center" direction="row" container>
                                  <Grid item container justifyContent="center" xs={12} sm={2}>
                                    <Radio value={o.id.toString()} color="primary" />
                                  </Grid>
                                  <Grid item xs={12} sm={10}>
                                    <OutletInfo outlet={o} />
                                  </Grid>
                                </Grid>
                              </ListItem>
                              <Divider />
                            </React.Fragment>
                          ))}
                        </RadioGroup>
                      </List>
                    </Card>
                  ) : (
                    <Grid xs={12} item justifyContent="center" container>
                      <Grid xs={12} sm={6} item direction="column" container>
                        <Typography align="center" variant="h5" className={classes.text}>
                          ?????????????????? ????????????????: {prepurchaseInfo?.deliveryPrice}???
                        </Typography>
                        <TextField
                          className={classes.field}
                          value={recieverName}
                          onChange={recieverNameChange}
                          variant="outlined"
                          label="?????? ????????????????????"
                        />
                        <TextField
                          className={classes.field}
                          value={recieverAdress}
                          onChange={recieverAdressChange}
                          variant="outlined"
                          label="?????????? ????????????????"
                        />
                      </Grid>
                    </Grid>
                  )}
                </Grid>
              </Card>
              <Card className={classes.card} variant="outlined">
                <Typography align="center" variant="h5" className={classes.pageName}>
                  ????????: {sum}???
                </Typography>
              </Card>
              <Card className={classes.card} variant="outlined">
                <Grid container justifyContent="center">
                  <Button color="primary" variant="contained" onClick={submit}>
                    ????????????
                  </Button>
                </Grid>
              </Card>
            </React.Fragment>
          )}
          {operationResult && (
            <React.Fragment>
              <PurchaseDetailedInfo purchase={operationResult} cardName={`?????????? ${operationResult.code} ????????????????:`} />
              <Button variant="contained" color="primary" href="/">
                ?? ????????????????
              </Button>
            </React.Fragment>
          )}
        </Grid>
      </Grid>
    </React.Fragment>
  );
};

export default PurchasingPage;
