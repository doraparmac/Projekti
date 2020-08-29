<?php
    class Dolazi{
        protected $id_event, $id_user;

        public function __construct($id_event, $id_user){
            $this->id_event = $id_event;
            $this->id_user = $id_user;
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