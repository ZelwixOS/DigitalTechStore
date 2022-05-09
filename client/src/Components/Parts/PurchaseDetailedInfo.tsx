import React from 'react';
import Typography from '@material-ui/core/Typography';
import { createStyles, makeStyles, Theme } from '@material-ui/core/styles';
import Grid from '@material-ui/core/Grid';
import ExpandMoreIcon from '@material-ui/icons/ExpandMore';
import { Accordion, AccordionDetails, AccordionSummary, Card, Divider, List, ListItem } from '@material-ui/core';

import Purchase from 'src/Types/Purchase';
import { getStatusString } from 'src/Types/PurchaseStatus';

import OutletInfo from './OutletInfo';
import PurchasingItemsList from './PurchasingItemsList';

interface IPurchaseDetailedInfo {
  purchase: Purchase;
  cardName: string;
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
  return (
    <Card className={classes.card} variant="outlined">
      <Grid xs={12} item justify="center" alignContent="flex-start" container>
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
              <Typography>Статус: {getStatusString(props.purchase.status)}</Typography>
            </ListItem>
            <Divider />
            <ListItem>
              <Typography>Общая стоимость: {props.purchase.totalCost}</Typography>
            </ListItem>
            <Divider />
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
                        <Typography>Стоимость доставки: {props.purchase.delivery.deliveryCost}</Typography>
                      </ListItem>
                      <Divider />
                    </List>
                  </Grid>
                ) : (
                  <React.Fragment>
                    <Typography>Магазин получения:</Typography>
                    <Grid container justify="center">
                      <OutletInfo outlet={props.purchase.deliveryOutlet} />
                    </Grid>
                  </React.Fragment>
                )}
              </AccordionDetails>
            </Accordion>
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
          </List>
        </Grid>
      </Grid>
    </Card>
  );
};

export default PurchaseDetailedInfo;
