﻿// Generated by IcedCoffeeScript 108.0.11
(function() {
  ContentTools.Event = (function() {
    function Event(name, detail) {
      this._name = name;
      this._detail = detail;
      this._timeStamp = Date.now();
      this._defaultPrevented = false;
      this._propagationStopped = false;
    }

    Event.prototype.defaultPrevented = function() {
      return this._defaultPrevented;
    };

    Event.prototype.detail = function() {
      return this._detail;
    };

    Event.prototype.name = function() {
      return this._name;
    };

    Event.prototype.propagationStopped = function() {
      return this._propagationStopped;
    };

    Event.prototype.timeStamp = function() {
      return this._timeStamp;
    };

    Event.prototype.preventDefault = function() {
      return this._defaultPrevented = true;
    };

    Event.prototype.stopImmediatePropagation = function() {
      return this._propagationStopped = true;
    };

    return Event;

  })();

}).call(this);
