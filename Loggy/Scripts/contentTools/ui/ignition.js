﻿// Generated by IcedCoffeeScript 108.0.11
(function() {
  var __hasProp = {}.hasOwnProperty,
    __extends = function(child, parent) { for (var key in parent) { if (__hasProp.call(parent, key)) child[key] = parent[key]; } function ctor() { this.constructor = child; } ctor.prototype = parent.prototype; child.prototype = new ctor(); child.__super__ = parent.prototype; return child; };

  ContentTools.IgnitionUI = (function(_super) {
    __extends(IgnitionUI, _super);

    function IgnitionUI() {
      IgnitionUI.__super__.constructor.call(this);
      this._revertToState = 'ready';
      this._state = 'ready';
    }

    IgnitionUI.prototype.busy = function(busy) {
      if (this.dispatchEvent(this.createEvent('busy', {
        busy: busy
      }))) {
        if (busy === (this._state === 'busy')) {
          return;
        }
        if (busy) {
          this._revertToState = this._state;
          return this.state('busy');
        } else {
          return this.state(this._revertToState);
        }
      }
    };

    IgnitionUI.prototype.cancel = function() {
      if (this.dispatchEvent(this.createEvent('cancel'))) {
        return this.state('ready');
      }
    };

    IgnitionUI.prototype.confirm = function() {
      if (this.dispatchEvent(this.createEvent('confirm'))) {
        return this.state('ready');
      }
    };

    IgnitionUI.prototype.edit = function() {
      if (this.dispatchEvent(this.createEvent('edit'))) {
        return this.state('editing');
      }
    };

    IgnitionUI.prototype.mount = function() {
      IgnitionUI.__super__.mount.call(this);
      this._domElement = this.constructor.createDiv(['ct-widget', 'ct-ignition', 'ct-ignition--ready']);
      this.parent().domElement().appendChild(this._domElement);
      this._domEdit = this.constructor.createDiv(['ct-ignition__button', 'ct-ignition__button--edit']);
      this._domElement.appendChild(this._domEdit);
      this._domConfirm = this.constructor.createDiv(['ct-ignition__button', 'ct-ignition__button--confirm']);
      this._domElement.appendChild(this._domConfirm);
      this._domCancel = this.constructor.createDiv(['ct-ignition__button', 'ct-ignition__button--cancel']);
      this._domElement.appendChild(this._domCancel);
      this._domBusy = this.constructor.createDiv(['ct-ignition__button', 'ct-ignition__button--busy']);
      this._domElement.appendChild(this._domBusy);
      return this._addDOMEventListeners();
    };

    IgnitionUI.prototype.state = function(state) {
      if (state === void 0) {
        return this._state;
      }
      if (this._state === state) {
        return;
      }
      if (!this.dispatchEvent(this.createEvent('statechange', {
        state: state
      }))) {
        return;
      }
      this._state = state;
      this.removeCSSClass('ct-ignition--busy');
      this.removeCSSClass('ct-ignition--editing');
      this.removeCSSClass('ct-ignition--ready');
      if (this._state === 'busy') {
        return this.addCSSClass('ct-ignition--busy');
      } else if (this._state === 'editing') {
        return this.addCSSClass('ct-ignition--editing');
      } else if (this._state === 'ready') {
        return this.addCSSClass('ct-ignition--ready');
      }
    };

    IgnitionUI.prototype.unmount = function() {
      IgnitionUI.__super__.unmount.call(this);
      this._domEdit = null;
      this._domConfirm = null;
      return this._domCancel = null;
    };

    IgnitionUI.prototype._addDOMEventListeners = function() {
      this._domEdit.addEventListener('click', (function(_this) {
        return function(ev) {
          ev.preventDefault();
          return _this.edit();
        };
      })(this));
      this._domConfirm.addEventListener('click', (function(_this) {
        return function(ev) {
          ev.preventDefault();
          return _this.confirm();
        };
      })(this));
      return this._domCancel.addEventListener('click', (function(_this) {
        return function(ev) {
          ev.preventDefault();
          return _this.cancel();
        };
      })(this));
    };

    return IgnitionUI;

  })(ContentTools.WidgetUI);

}).call(this);
