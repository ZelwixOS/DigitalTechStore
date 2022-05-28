import React, { useState } from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import {
  Accordion,
  AccordionDetails,
  AccordionSummary,
  Button,
  Card,
  Divider,
  List,
  ListItem,
} from '@material-ui/core';

import Purchase from 'src/Types/Purchase';
import { getStatusString, PurchaseStatus } from 'src/Types/PurchaseStatus';
import { updatePurchaseStatus, cancelPurchaseStatus } from 'src/Requests/PutRequests';

import OutletInfo from './OutletInfo';
import PurchasingItemsList from './PurchasingItemsList';

interface IPurchaseDetailedInfo {
  purchase: Purchase;
  cardName: string;
  hideDelivery?: boolean;
  showRefuse?: boolean;
  showCancel?: boolean;
  showFinish?: boolean;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    card: {
      marginTop: theme.spacing(2),
      padding: theme.spacing(2),
    },
    text: {
      marginTop: theme.spacing(1),
      marginBottom: theme.spacing(1),
    },
  }),
);

const PurchaseDetailedInfo: React.FC<IPurchaseDetailedInfo> = props => {
  const classes = useStyles();

  const [status, setStatus] = useState(props.purchase.status);

  const completed = async () => {
    const res = await updatePurchaseStatus(props.purchase.id, PurchaseStatus.finished);
    if (res > 0) {
      setStatus(PurchaseStatus.finished);
    }
  };

  const cancel = async () => {
    const res = await cancelPurchaseStatus(props.purchase.id);
    if (res > 0) {
      setStatus(PurchaseStatus.canceledByClient);
    }
  };

  const refuse = async () => {
    const res = await updatePurchaseStatus(props.purchase.id, PurchaseStatus.refused);
    if (res > 0) {
      setStatus(PurchaseStatus.refused);
    }
  };

  return (
    <Card className={classes.card} variant="outlined">
      <Grid xs={12} item justifyContent="center" alignContent="flex-start" container>
        <Grid xs={12} sm={9} item direction="column" container>
          <Typography align="center" variant="h5" className={classes.text}>
            {props.cardName}
          </Typography>
          <List>
            <Divider />
            <ListItem>
              <Typography>Покупатель: {props.purchase.customerName}</Typography>
            </ListItem>
            <Divider />
            {props.purchase.sellerName && (
              <React.Fragment>
                <ListItem>
                  <Typography>Продавец: {props.purchase.sellerName}</Typography>
                </ListItem>
                <Divider />
              </React.Fragment>
            )}
            <ListItem>
              <Typography>Статус: {getStatusString(status)}</Typography>
            </ListItem>
            <Divider />
            <ListItem>
              <Typography>Общая стоимость: {props.purchase.totalCost}₽</Typography>
            </ListItem>
            <Divider />
            {!props.hideDelivery && (
              <Accordion variant="outlined">
                <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                  <Typography>Доставка</Typography>
                </AccordionSummary>
                <AccordionDetails>
                  {props.purchase.delivery ? (
                    <Grid xs={12} item>
                      <List>
                        <ListItem>
                          <Typography>Получатель: {props.purchase.delivery.recieverName}</Typography>
                        </ListItem>
                        <Divider />
                        <ListItem>
                          <Typography>Адрес доставки: {props.purchase.delivery.deliveryAdress}</Typography>
                        </ListItem>
                        <Divider />
                        <ListItem>
                          <Typography>Стоимость доставки: {props.purchase.delivery.deliveryCost}₽</Typography>
                        </ListItem>
                        <Divider />
                      </List>
                    </Grid>
                  ) : (
                    <React.Fragment>
                      <Typography>Магазин получения:</Typography>
                      <Grid container justifyContent="center">
                        <OutletInfo outlet={props.purchase.deliveryOutlet} />
                      </Grid>
                    </React.Fragment>
                  )}
                </AccordionDetails>
              </Accordion>
            )}
            <Accordion variant="outlined">
              <AccordionSummary expandIcon={<ExpandMoreIcon />}>
                <Typography>Список покупок</Typography>
              </AccordionSummary>
              <AccordionDetails>
                <Grid item xs={12}>
                  <PurchasingItemsList purchaseItems={props.purchase.purchaseItems} />
                </Grid>
              </AccordionDetails>
            </Accordion>
            <ListItem>
              <Grid container direction="row" justifyContent="space-evenly">
                {props.showCancel &&
                  status !== PurchaseStatus.refused &&
                  status !== PurchaseStatus.canceledByClient &&
                  status !== PurchaseStatus.finished && (
                    <Button variant="contained" onClick={cancel}>
                      Отменить
                    </Button>
                  )}
                {props.showFinish &&
                  status !== PurchaseStatus.refused &&
                  status !== PurchaseStatus.canceledByClient &&
                  status !== PurchaseStatus.finished && (
                    <Button variant="contained" onClick={refuse}>
                      Отказ
                    </Button>
                  )}
                {props.showRefuse &&
                  status !== PurchaseStatus.refused &&
                  status !== PurchaseStatus.canceledByClient &&
                  status !== PurchaseStatus.finished && (
                    <Button variant="contained" color="primary" onClick={completed}>
                      Выполнено
                    </Button>
                  )}
              </Grid>
            </ListItem>
          </List>
        </Grid>
      </Grid>
    </Card>
  );
};

export default PurchaseDetailedInfo;
