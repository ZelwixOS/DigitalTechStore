import React from 'react';
import Grid from '@material-ui/core/Grid';
import Container from '@material-ui/core/Container';
import Box from '@material-ui/core/Box';
import Link from '@material-ui/core/Link';
import { createStyles, makeStyles, Theme, Typography } from '@material-ui/core';

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      marginTop: theme.spacing(10),
      color: '#CCC',
      backgroundColor: '#444',
    },
  }),
);

const Footer = () => {
  const classes = useStyles();

  return (
    <footer className={classes.root}>
      <Box>
        <Container maxWidth="lg">
          <Grid container spacing={5}>
            <Grid item xs={12} sm={4}>
              <Box borderBottom={1}>Полезные ссылки</Box>
              <Box>
                <Link href="/Shops">Магазины</Link>
              </Box>
              <Box>
                <Link href="/Registration">Регистрация</Link>
              </Box>
            </Grid>
            <Grid item xs={12} sm={4}>
              <Box borderBottom={1}>Поддержка</Box>
              <Box>
                <Typography>
                  E-mail: <Link href="mailto:techno-dts@mail.ru">techno-dts@mail.ru</Link>
                </Typography>
              </Box>
              <Box>
                <Typography>
                  Горячая линия: <Link href="tel:8(800)735-21-48">8(800)735-21-48</Link>
                </Typography>
              </Box>
            </Grid>
            <Grid item xs={12} sm={4}>
              <Box borderBottom={1}>О приложении</Box>
              <Box>Все права на приложение интернет-магазина</Box>
              <Box>принадлежат DTS© 2022</Box>
            </Grid>
          </Grid>
        </Container>
      </Box>
    </footer>
  );
};

export default Footer;
