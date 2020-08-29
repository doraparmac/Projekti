<?php
    class Comment{
        protected $id, $id_user, $id_event, $opis, $zvjezdice, $vrijeme_objave;

        public function __construct($id, $id_user, $id_event, $opis, $zvjezdice, $vrijeme_objave){
            $this->id = $id;
            $this->id_user = $id_user;
            $this->id_event = $id_event;
            $this->opis = $opis;
            $this->zvjezdice = $zvjezdice;
            $this->vrijeme_objave = $vrijeme_objave;
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