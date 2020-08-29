<?php
    class Event{
        protected $id, $id_user, $dolazi, $mjesto, $grad, $kategorija, $vrijeme_pocetak, 
                    $vrijeme_kraj, $datum_pocetak, $datum_kraj, $naslov, $opis;

        public function __construct ($id, $id_user, $dolazi, $mjesto, $grad, $kategorija, 
                $vrijeme_pocetak, $vrijeme_kraj, $datum_pocetak, $datum_kraj, $naslov, $opis){
            $this->id = $id;
            $this->id_user = $id_user;
            $this->dolazi = $dolazi;
            $this->mjesto = $mjesto;
            $this->grad = $grad;
            $this->kategorija = $kategorija;
            $this->vrijeme_pocetak = $vrijeme_pocetak;
            $this->vrijeme_kraj = $vrijeme_kraj;
            $this->datum_pocetak = $datum_pocetak;
            $this->datum_kraj = $datum_kraj;
            $this->naslov = $naslov;
            $this->opis = $opis;
        }

        public function __get( $property ){
            if( property_exists( $this, $property ) )
                return $this->$property; 
        }
    
        public function __set( $property, $value ){
            if( property_exists( $this, $property ) )
                $this->$property = $value;

            return $this;
        }
    }
?>